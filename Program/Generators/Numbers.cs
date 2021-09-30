using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Program.Generators
{
    public class Numbers : IValueGenerator
    {
        public object Generate(GeneratorContext context)
        {
            if (context.TargetType.Equals(typeof(int)))
            {
                return context.Random.Next(0, int.MaxValue);
            }
            if (context.TargetType.Equals(typeof(long)))
            {
                var nextVal = context.Random.NextDouble();
                nextVal = nextVal - Math.Truncate(nextVal);
                return (long)(nextVal * long.MaxValue);
            }
            if (context.TargetType.Equals(typeof(float)))
            {
                var nextVal = context.Random.NextDouble();
                var range = float.MaxValue;
                return (float)(nextVal * range);
            }
            if (context.TargetType.Equals(typeof(double)))
            {
                var nextVal = context.Random.NextDouble();
                return nextVal * double.MaxValue;
            }
            if (context.TargetType.Equals(typeof(short)))
            {
                var nextVal = context.Random.NextDouble();
                nextVal = nextVal - Math.Truncate(nextVal);

                return (short)(nextVal * short.MaxValue);
            }
                return null;
        }

        public bool CanGenerate(Type type)
        {
            return type.Equals(typeof(int)) || type.Equals(typeof(long)) || type.Equals(typeof(float)) || type.Equals(typeof(double)) || type.Equals(typeof(short));
        }
    }
}

//The primitive types are Boolean, Byte, SByte, Int16, UInt16, Int32, UInt32, Int64, UInt64, IntPtr, UIntPtr, Char, Double, and Single.