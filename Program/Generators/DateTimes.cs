using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Program.Generators
{
    public class DateTimes : IValueGenerator
    {
        public object Generate(GeneratorContext context)
        {
            DateTime from = DateTime.Now.AddYears(-70);
            DateTime to = DateTime.Now.AddYears(70);
            var span = new TimeSpan(to.Ticks - from.Ticks);
            return from + new TimeSpan((long)(span.Ticks * context.Random.NextDouble()));
        }

        public bool CanGenerate(Type type)
        {
            return type.Equals(typeof(DateTime));
        }
    }
}
