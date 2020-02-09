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



            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new AppView());

            //Console.WriteLine(resultsPresentation.calculationDurationMS);
        }
    }
}
