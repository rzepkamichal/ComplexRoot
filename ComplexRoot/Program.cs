using ComplexRoot.complex_num;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;



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

            List<ComplexNumAlgebraic> numbers;


            string jsonString;
            jsonString = File.ReadAllText("output.json");

            numbers = JsonConvert.DeserializeObject<List<ComplexNumAlgebraic>>(jsonString);

            List<ComplexRootResultPresentation> results = ComplexNumUtils.calculateRoots(null);

            string resultsJson = JsonConvert.SerializeObject(results, Formatting.Indented);
            File.WriteAllText("results.json", resultsJson);

        }
    }
}
