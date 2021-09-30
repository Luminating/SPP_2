using System;
using System.Collections.Generic;
using System.Linq;

namespace Program
{
    class Program
    {
        static void Main(string[] args)
        {
            Faker faker = new Faker();
            int var1 = faker.Create<int>();
            String var2 = faker.Create<String>();
            List<int> var3 = faker.Create<List<int>>();
            DateTime var4 = faker.Create<DateTime>();
            Char ch = faker.Create<char>();
            Byte bt = faker.Create<byte>();

            Foo foo = faker.Create<Foo>();
        }
    }
}
