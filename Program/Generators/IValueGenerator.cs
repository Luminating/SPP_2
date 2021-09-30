using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Program.Generators
{
    public interface IValueGenerator
    {
        object Generate(GeneratorContext context);
        bool CanGenerate(Type type);
    }
}
