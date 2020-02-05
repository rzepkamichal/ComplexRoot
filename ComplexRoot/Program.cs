using ComplexRoot.complex_num;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;

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

            string inputJson;
            inputJson = File.ReadAllText("input.json");

            List<ComplexAlgebraic> inputs = JsonConvert.DeserializeObject<List<ComplexAlgebraic>>(inputJson);
            ResultsPresentation resultsPresentation = ComplexNumUtils.calculateRoots(inputs, 5);

            string resultsJson = JsonConvert.SerializeObject(resultsPresentation, Formatting.Indented);
            File.WriteAllText("output.json", resultsJson);

            Console.WriteLine(resultsPresentation.calculationDurationMS);

            


        }
    }
}
