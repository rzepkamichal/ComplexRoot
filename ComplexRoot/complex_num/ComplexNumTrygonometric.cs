using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplexRoot.complex_num
{
    /// <summary>
    /// Trygonometric representation of a complex number
    /// </summary>
    class ComplexNumTrygonometric
    {
        /// <summary>
        /// Modulus of the complex number
        /// </summary>
        public double modulus { get; set; }

        /// <summary>
        /// Arc of the complex number
        /// </summary>
        public double arc { get; set; }

        /// <summary>
        /// The root to be calculated
        /// </summary>
        public int root { get; set; }

        public ComplexNumTrygonometric(double modulus, double arc, int root)
        {
            this.modulus = modulus;
            this.arc = arc;
            this.root = root;
        }
    }
}
