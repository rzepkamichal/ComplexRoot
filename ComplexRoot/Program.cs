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

        [DllImport("ComplexRootLibCpp.dll", CallingConvention = CallingConvention.Cdecl)]
        static extern IntPtr calculateRoots(double modulus, double arc, int n);

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            double modulus = 1;
            double arc = 0;
            int n = 4;
            
            IntPtr libResult = calculateRoots(modulus, arc, n);
            double[] results = new double[2 * n];
            Marshal.Copy(libResult, results, 0, 2 * n);

            for(int i = 0; i < 2 * n; i++)
                Console.WriteLine(String.Format("{0:0.###############}", results[i]));
            

            //Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new Form1());
        }
    }
}
