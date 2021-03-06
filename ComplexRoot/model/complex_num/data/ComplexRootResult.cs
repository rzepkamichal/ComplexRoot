﻿/*
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
