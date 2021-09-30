using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Program.Generators
{
    class Booleans : IValueGenerator
    {
        public object Generate(GeneratorContext context)
        {
            var num = (int)Math.Round(context.Random.NextDouble());
            return num != 0;
        }
        public bool CanGenerate(Type type)
        {
            return type.Equals(typeof(Boolean));
        }

    }
}
