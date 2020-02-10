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
