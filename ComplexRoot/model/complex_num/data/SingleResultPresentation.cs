using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplexRoot.complex_num
{
    class SingleResultPresentation
    {
        public ComplexAlgebraic input { get; set; }
        public List<ComplexRootResult> results { get; set; }

        public SingleResultPresentation()
        {
            this.results = new List<ComplexRootResult>();
        }
    }
}
