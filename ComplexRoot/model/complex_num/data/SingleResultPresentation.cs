using System.Collections.Generic;

namespace ComplexRoot.complex_num
{
    /// <summary>
    /// Representation of resulting roots calculated for one complex number.
    /// </summary>
    class SingleResultPresentation
    {
        /// <summary>
        /// Reference to the input number.
        /// </summary>
        public ComplexAlgebraic input { get; set; }

        /// <summary>
        /// List of results.
        /// </summary>
        public List<ComplexRootResult> results { get; set; }

        /// <summary>
        /// Default constructor.
        /// Initializes empty list of results.
        /// </summary>
        public SingleResultPresentation()
        {
            this.results = new List<ComplexRootResult>();
        }
    }
}
