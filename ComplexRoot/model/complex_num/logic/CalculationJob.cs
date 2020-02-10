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
