using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ComplexRoot
{
    static class Program
    {

        [DllImport("ComplexRootLibCpp.dll")]
        static extern unsafe void calculateRoots(double modulus, double arc, int n, double[] results);

        [DllImport("ComplexRootLibAsm.dll")]
        static extern unsafe double calculateRootsAsm();

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {

            double modulus = 13;
            double arc = 23;
            int n = 2;

            double[] results = new double[2 * n];
            calculateRoots(modulus, arc, n, results);
            //calculateRootsAsm();


            //for (int i = 0; i < 2 * n; i++)
            //    Console.WriteLine(String.Format("{0:0.###############}", results[i]));

            
            Console.WriteLine("ZWROC NUMBER: " + calculateRootsAsm());
            //Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new Form1());
        }
    }
}
