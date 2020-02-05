using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplexRoot.complex_num
{
    class ComplexRootResult
    {
        public double re { get; set; }
        public double im { get; set; }

        public ComplexRootResult(double re, double im)
        {
            this.re = re;
            this.im = im;
        }
    }
}
