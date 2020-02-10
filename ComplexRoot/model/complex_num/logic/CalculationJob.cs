using System.Collections.Generic;
using System.Threading;

namespace ComplexRoot.complex_num
{
    /// <summary>
    /// Represents a complex root calculation job.
    /// </summary>
    class CalculationJob
    {
        /// <summary>
        /// Reference to the thread, on which the calculations will be run.
        /// </summary>
        public Thread thread { get; set; }

        /// <summary>
        /// List of input numbers.
        /// </summary>
        public List<ComplexAlgebraic> inputs { get; set; }

        /// <summary>
        /// List of results.
        /// </summary>
        public List<SingleResultPresentation> resultPresenations { get; set; }

        /// <summary>
        /// Default constructor. 
        /// Initializes empty input and result list.
        /// </summary>
        public CalculationJob()
        {
            this.inputs = new List<ComplexAlgebraic>();
            this.resultPresenations = new List<SingleResultPresentation>();
        }
    }
}
