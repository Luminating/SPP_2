using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace Program
{
    class Foo
    {
        public int var1;
        public String var2 { get; set; }
        private DateTime var3;
        private List<String> var4;
        public char var5;
        public byte var6;
        public Boolean var7;

        public Foo(DateTime var3, List<string> var4)
        {
            this.var3 = var3;
            this.var4 = var4;
        }

        public Foo(string var2, DateTime var3, List<string> var4)
        {
            this.var2 = var2;
            this.var3 = var3;
            this.var4 = var4;
        }
    }
}
