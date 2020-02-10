/*
Autor: Michał Rzepka
Rodzaj studiów: SSI
Kierunek: Informatyka
Semestr: 5
Grupa dziekańska: 1
Sekcja lab: 2
Przedmiot: Języki Asemblerowe
Email: michrze558@student.polsl.pl
Temat projektu: Wyznaczanie pierwiastków liczb zespolonych
Data oddania projektu: 10-02-2020
*/

/* CHANGELOG
 * 13-11-2019 init project
 * 04-02-2020 create model
 * 10-02-2020 add documentation
 */
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
