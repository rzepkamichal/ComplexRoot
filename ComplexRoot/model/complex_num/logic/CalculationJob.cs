using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ComplexRoot.complex_num
{
    class CalculationJob
    {
        public Thread thread { get; set; }
        public List<ComplexAlgebraic> inputs { get; set; }
        public List<SingleResultPresentation> resultPresenations { get; set; }

        public CalculationJob()
        {
            this.inputs = new List<ComplexAlgebraic>();
            this.resultPresenations = new List<SingleResultPresentation>();
        }
    }
}
