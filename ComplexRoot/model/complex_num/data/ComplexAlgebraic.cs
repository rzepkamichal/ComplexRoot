namespace ComplexRoot.complex_num
{
    /// <summary>
    /// Algebraic representation of a complex nubmer.
    /// </summary>
    public class ComplexAlgebraic
    {
        /// <summary>
        /// Real part of the complex number.
        /// </summary>
        public double re { get; set; }

        /// <summary>
        /// Imaginary part of the complex number.
        /// </summary>
        public double im { get; set; }

        /// <summary>
        /// Root to be calculated.
        /// </summary>
        public ushort root { get; set; }

        /// <summary>
        /// Parametric constructor.
        /// </summary>
        /// <param name="re">Real part of the number.</param>
        /// <param name="im">Imaginary part of the number.</param>
        /// <param name="root">Root to be calculated.</param>
        public ComplexAlgebraic(double re, double im, ushort root)
        {
            this.re = re;
            this.im = im;
            this.root = root;
   
        }
    }
}
