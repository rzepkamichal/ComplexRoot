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
    /// Aggregate representation of root results calculated for all input complex numbers.
    /// </summary>
    class ResultsPresentation
    {
        /// <summary>
        /// Information about calculation duration.
        /// </summary>
        public double calculationDurationMS { get; }

        /// <summary>
        /// List of results calculated for each input number.
        /// </summary>
        public List<SingleResultPresentation> results { get; }

        /// <summary>
        /// Parametric constructor.
        /// </summary>
        /// <param name="results">Initial result list.</param>
        /// <param name="calculationDurationMS">Calculation duration.</param>
        public ResultsPresentation(List<SingleResultPresentation> results, double calculationDurationMS)
        {
            this.calculationDurationMS = calculationDurationMS;
            this.results = results;
        }
    }
}
