namespace ComplexRoot.complex_num
{
    /// <summary>
    /// Algebraic representation of a complex nubmer
    /// </summary>
    
    public class ComplexAlgebraic
    {
        /// <summary>
        /// Real part of the complex number
        /// </summary>
        public double re { get; set; }

        /// <summary>
        /// Imaginary part of the complex number
        /// </summary>
        public double im { get; set; }

        /// <summary>
        /// Root to be calculated
        /// </summary>
        public int root { get; set; }

        public ComplexAlgebraic(double re, double im, int root)
        {
            this.re = re;
            this.im = im;
            this.root = root;
   
        }
    }
}
