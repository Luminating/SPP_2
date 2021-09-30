using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Program.Generators
{
    public class Strings : IValueGenerator
    {
        private const string Chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

        public object Generate(GeneratorContext context)
        {
            var stringLength = context.Random.Next(10, 40);
            return new string(Enumerable.Repeat(Chars, stringLength).Select(x => x[context.Random.Next(x.Length)]).ToArray());
        }

        public bool CanGenerate(Type type)
        {
            return type.Equals(typeof(string));
        }
    }
}
