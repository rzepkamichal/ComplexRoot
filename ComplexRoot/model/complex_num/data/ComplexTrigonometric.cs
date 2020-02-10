namespace ComplexRoot.complex_num
{
    /// <summary>
    /// Trygonometric representation of a complex number.
    /// </summary>
    class ComplexTrigonometric
    {
        /// <summary>
        /// Modulus of the complex number.
        /// </summary>
        public double modulus { get; set; }

        /// <summary>
        /// Argument of the complex number
        /// </summary>
        public double arc { get; set; }

        /// <summary>
        /// The root to be calculated.
        /// </summary>
        public ushort root { get; set; }

        /// <summary>
        /// Parametric constructor.
        /// </summary>
        /// <param name="modulus">Modulus of the trigonometric representation.</param>
        /// <param name="arc">Argument of the trigonometric representation.</param>
        /// <param name="root">Root to be calculated.</param>
        public ComplexTrigonometric(double modulus, double arc, ushort root)
        {
            this.modulus = modulus;
            this.arc = arc;
            this.root = root;
        }
    }
}
