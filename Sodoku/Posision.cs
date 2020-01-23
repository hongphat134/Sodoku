using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sodoku
{
    public class Posision
    {
        public int a;
        public int b;
        public int c;
        public int d;

        public Posision()
        {
            this.a = 0; this.b = 0; this.c = 0; this.d = 0;
        }
        public Posision(int a, int b, int c, int d)
        {
            this.a = a;
            this.b = b;
            this.c = c;
            this.d = d;
        }

        public String ToString()
        {
            return "(" + this.a + "," + this.b + "," + this.c + "," + this.d + ")";
        }
    }
}
