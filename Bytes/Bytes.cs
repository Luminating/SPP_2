using System;
using Program.Generators;

namespace Bytes
{
    public class Bytes : IValueGenerator
    {
        public object Generate(GeneratorContext context)
        {
            var nextVal = context.Random.NextDouble();
            nextVal = nextVal - Math.Truncate(nextVal);

            return (byte)(nextVal * byte.MaxValue);
        }
        public bool CanGenerate(Type type)
        {
            return type.Equals(typeof(byte));
        }
    }
}
