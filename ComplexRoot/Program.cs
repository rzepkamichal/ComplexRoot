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
 * 13-11-2019 add c++ library project
 * 13-11-2919 implement c++ library
 * 30-12-2019 refactor c++ library, initialize asm project
 * 07-01-2020 add asm library calling
 * 28-01-2020 add asm n-th real root calculation
 * 02-02-2020 complete asm library implementation
 * 03-02-2020 refactor asm library
 * 04-02-2020 create model
 * 04-02-2020 add json i/o support
 * 04-02-2020 add thread support
 * 05-02-2020 fix asm multithreading bug (replace .data section with stack usage)
 * 05-02-2020 create GUI dialog
 * 05-02-2020 add dialog control listeners
 * 09-02-2020 provide input data and enviroment validation
 * 10-02-2020 rework to put result list in the same order as the input list
 * 10-02-2020 add documentation
 */

using System;
using System.Windows.Forms;

namespace ComplexRoot
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //run the view
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new AppView());
        }
    }
}
