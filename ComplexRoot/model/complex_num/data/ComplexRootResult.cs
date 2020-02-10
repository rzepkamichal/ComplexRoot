namespace ComplexRoot.complex_num
{
    /// <summary>
    /// Representation of one of resulting roots calculated fo a single complex number.
    /// </summary>
    class ComplexRootResult
    {
        //real part of resulting number
        public double re { get; set; }

        //imaginary part of resulting number
        public double im { get; set; }

        /// <summary>
        /// Parametric constructor
        /// </summary>
        /// <param name="re">Real part of complex number.</param>
        /// <param name="im">Imaginary part of complex number.</param>
        public ComplexRootResult(double re, double im)
        {
            this.re = re;
            this.im = im;
        }
    }
}
