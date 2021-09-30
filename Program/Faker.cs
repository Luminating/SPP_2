using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using Program.Generators;

namespace Program
{
    public class Faker
    {
        private static readonly Random random = new Random();
        private static readonly string pluginPath = Path.Combine(Directory.GetCurrentDirectory(), "Plugins");
        private static readonly List<IValueGenerator> generators = new List<IValueGenerator>() {
            new Booleans(),
            new DateTimes(),
            new Numbers(),
            new Strings(),
            new Lists()
        };
        private List<Type> _generatedTypes = new List<Type>();

        static Faker()
        {
            string[] pluginFiles = Directory.GetFiles(pluginPath, "*.dll");
            foreach (string file in pluginFiles)
            {
                Assembly asm = Assembly.LoadFrom(file);
                IEnumerable<Type> types = asm.GetTypes().
                                   Where(t => t.GetInterfaces().
                                   Where(i => i.FullName == typeof(IValueGenerator).FullName).Any());

                foreach (Type type in types)
                {
                    IValueGenerator plugin = asm.CreateInstance(type.FullName) as IValueGenerator;
                    generators.Add(plugin);
                }
            }
        }

        public T Create<T>()
        {
            return (T)Create(typeof(T));
        }

        private object Create(Type type)
        {
            IValueGenerator generator = GetGenerator(type);
            if (generator != null)
            {
                GeneratorContext context = new GeneratorContext(random, type, this);
                return generator.Generate(context);
            }
            return CreateObject(type);
        }

        private object CreateObject(Type type)
        {
            if (IsСyclicalВependence(type))
            {
                return null;
            }

            ConstructorInfo[] constructorInfos = GetSortedConstructors(type);
            foreach (ConstructorInfo constructorInfo in constructorInfos)
            {
                object createdObject = null;
                try
                {
                    object[] constructorArguments = CreateConstructorArguments(constructorInfo.GetParameters());
                    createdObject = Activator.CreateInstance(type, constructorArguments);
                }
                catch (NotSupportedException)
                {
                    break;
                }
                CreatePublicFieldsValues(createdObject, type);
                CreatePropertyValues(createdObject, type);
                return createdObject;
            }
            return null;
        }

        private object[] CreateConstructorArguments(ParameterInfo[] parameterInfos)
        {
            List<object> arguments = new List<object>();

            foreach (ParameterInfo parameterInfo in parameterInfos)
            {
                object generatedArgument = Create(parameterInfo.ParameterType);
                arguments.Add(generatedArgument);
            }

            return arguments.ToArray();
        }

        private ConstructorInfo[] GetSortedConstructors(Type type)
        { 
            return type.GetConstructors().OrderBy(x => x.GetParameters().Length).Reverse().ToArray();
        }

        private void CreatePublicFieldsValues(object obj, Type type) {
            FieldInfo[] fieldInfos = type.GetFields();

            foreach (FieldInfo fieldInfo in fieldInfos)
            {
                fieldInfo.SetValue(obj, Create(fieldInfo.FieldType));
            }
        }

        private void CreatePropertyValues(object obj, Type type)
        {
            PropertyInfo[] propertyInfos = type.GetProperties();

            foreach (PropertyInfo propertyInfo in propertyInfos)
            {
                if (propertyInfo.CanWrite)
                {
                    propertyInfo.SetValue(obj, Create(propertyInfo.PropertyType));
                }
            }
        }

        private IValueGenerator GetGenerator(Type type)
        {
            foreach (IValueGenerator generator in generators)
            {
                if (generator.CanGenerate(type))
                {
                    return generator;
                }
            }
            return null;
        }

        private bool IsСyclicalВependence(Type type)
        {
            foreach (Type generatedType in _generatedTypes)
            {
                if (generatedType.Equals(type))
                {
                    return true;
                }
            }
            _generatedTypes.Add(type);
            return false;
        }
    }
}
