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


            string inputJson;
            inputJson = File.ReadAllText("input.json");

            List<ComplexNumAlgebraic> algebraicInputs = JsonConvert.DeserializeObject<List<ComplexNumAlgebraic>>(inputJson);
            List<ComplexNumTrygonometric> trygInputs = new List<ComplexNumTrygonometric>();

            algebraicInputs.ForEach(input => trygInputs.Add(ComplexNumUtils.toTrygonometric(input)));
            ResultsPresentation resultsPresentation = ComplexNumUtils.calculateRoots(trygInputs, 2);

            //string resultsJson = JsonConvert.SerializeObject(resultsPresentation, Formatting.Indented);
            //File.WriteAllText("output.json", resultsJson);

            Console.WriteLine(resultsPresentation.calculationDurationMS);
        }
    }
}
