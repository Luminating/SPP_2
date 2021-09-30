using System;
using Program.Generators;

namespace Chars
{
    public class Chars : IValueGenerator
    {
        public object Generate(GeneratorContext context)
        {
            return (char)context.Random.Next('a', 'z');
        } 
        public bool CanGenerate(Type type)
        {
            return type.Equals(typeof(char));
        }
    }
}
