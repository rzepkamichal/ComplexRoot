using ComplexRoot.complex_num;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
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

            //string inputJson;
            //inputJson = File.ReadAllText("input.json");

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new AppView());

            //List<ComplexAlgebraic> inputs = JsonConvert.DeserializeObject<List<ComplexAlgebraic>>(inputJson);
            //ResultsPresentation resultsPresentation = ComplexNumUtils.calculateRoots(inputs, 17);

            //string resultsJson = JsonConvert.SerializeObject(resultsPresentation, Formatting.Indented);
            //File.WriteAllText("output.json", resultsJson);


            //Console.WriteLine(resultsPresentation.calculationDurationMS);
        }
    }
}
