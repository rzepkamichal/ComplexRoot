using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplexRoot.complex_num
{
    class ResultsPresentation
    {
        public double calculationDurationMS { get; set; }
        public List<SingleResultPresentation> results { get; set; }

        public ResultsPresentation(List<SingleResultPresentation> results, double calculationDurationMS)
        {
            this.calculationDurationMS = calculationDurationMS;
            this.results = results;
        }
    }
}
