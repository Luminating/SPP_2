using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace Program.Generators
{
    class Lists : IValueGenerator
    {
        public object Generate(GeneratorContext context)
        {
            Type[] genericType = context.TargetType.GetGenericArguments();
            Type listType = typeof(List<>).MakeGenericType(genericType);

            MethodInfo genericFakerCreateMethod = typeof(Faker).GetMethod("Create").MakeGenericMethod(genericType);

            object faker = new Faker();
            object list = Activator.CreateInstance(listType);

            int listLength = context.Random.Next(1, byte.MaxValue);
            for (int i = 0; i < listLength; i++)
            {
                object item = genericFakerCreateMethod.Invoke(faker, null);
                listType.GetMethod("Add").Invoke(list, new[] { item });
            }

            return list;
        }

        public bool CanGenerate(Type type)
        {
            return type.IsGenericType && !type.IsGenericTypeDefinition && type.GetGenericTypeDefinition().Equals(typeof(List<>));
        }
    }
}
