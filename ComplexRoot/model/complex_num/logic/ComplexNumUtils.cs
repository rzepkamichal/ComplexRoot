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
 * 04-02-2020 create model
 * 04-02-2020 add thread support
 * 05-02-2020 fix asm multithreading bug (replace .data section with stack usage)
 * 10-02-2020 rework to put result list in the same order as the input list
 * 10-02-2020 add documentation
 */
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

namespace ComplexRoot.complex_num
{
    /// <summary>
    /// Represents the type of chosen calculation library
    /// </summary>
    public enum LibraryType
    {
        //assembly library
        ASM,

        //c++ library
        CPP
    }

    /// <summary>
    /// Provides calculation utilities related to complex numbers.
    /// </summary>
    class ComplexNumUtils
    {
        /// <summary>
        /// Reference to the c++ dll functionality. Calculates all roots for the given 
        /// complex number in trigonometric representation.
        /// </summary>
        /// <param name="modulus">Modulus of the trigonometric representation.</param>
        /// <param name="arc">The argument of the trigonometric representation in radians.</param>
        /// <param name="n">The n-th root to be calculated.</param>
        /// <param name="results">Pointer to array that should store the results. 
        /// Each resulting complex number is stored in two adjacent elements - first the real part, then the imaginary part.
        /// Therefore the size of the array should be equal to 2n.</param>
        [DllImport("ComplexRootLibCpp.dll", CallingConvention = CallingConvention.StdCall)]
        public unsafe static extern void calculateRootsCpp(double modulus, double arc, int n, double* results);

        /// <summary>
        /// Reference to the assembly dll functionality. Calculates all roots for the given 
        /// complex number in trigonometric representation.
        /// </summary>
        /// <param name="modulus">Modulus of the trigonometric representation.</param>
        /// <param name="arc">The argument of the trigonometric representation in radians.</param>
        /// <param name="n">The n-th root to be calculated.</param>
        /// <param name="results">Pointer to array that should store the results. 
        /// Each resulting complex number is stored in two adjacent elements - first the real part, then the imaginary part.
        /// Therefore the size of the array should be equal to 2n.</param>
        [DllImport("ComplexRootLibAsm.dll", CallingConvention = CallingConvention.StdCall)]
        public unsafe static extern void calculateRootsAsm(double modulus, double arc, int n, double* results);

        /// <summary>
        /// Loads the .dll library of given name.
        /// </summary>
        /// <param name="lpFileName">Name of the library to be loaded.</param>
        /// <returns>IntPtr.Zero if the library failed to load or an IntPtr to the library otherwise.</returns>
        [DllImport("kernel32", SetLastError = true)]
        private static extern IntPtr LoadLibrary(string lpFileName);

        /// <summary>
        /// Checks whether the C++ and Assembly libraries are available.
        /// </summary>
        /// <returns>True if both libraries are available.</returns>
        public static bool areLibrariesPresent()
        {
            return LoadLibrary("ComplexRootLibCpp.dll") != IntPtr.Zero
                && LoadLibrary("ComplexRootLibAsm.dll") != IntPtr.Zero;
        }

        /// <summary>
        /// Converts the given algebraic representation of complex number 
        /// to the equivalent trigonometric representation.
        /// </summary>
        /// <param name="algebraic">Algebraic representation of the complex number.</param>
        /// <returns>Trigonometric representation of the complex number</returns>
        public static ComplexTrigonometric toTrygonometric(ComplexAlgebraic algebraic)
        {
            //calculate the modulus
            double modulus = Math.Sqrt(Math.Pow(algebraic.im, 2.0) + Math.Pow(algebraic.re, 2.0));

            //calculate the trigonometric argument
            double arc = 0;
            if (algebraic.re > 0 && algebraic.im != 0)
            {
                arc = Math.Atan(algebraic.im / algebraic.re);
            }
            else if (algebraic.re < 0 && algebraic.im >= 0)
            {
                arc = Math.Atan(algebraic.im / algebraic.re) + Math.PI;
            }
            else if (algebraic.re < 0 && algebraic.im < 0)
            {
                arc = Math.Atan(algebraic.im / algebraic.re) - Math.PI;
            }

            return new ComplexTrigonometric(modulus, arc, algebraic.root);
        }

        /// <summary>
        /// Converts the given trigonometric representation of a complex number to its algebraic representation.
        /// </summary>
        /// <param name="trygonometric">Trygonometric representation of the complex number.</param>
        /// <returns>Algebraic representation of the complex number.</returns>
        public static ComplexAlgebraic toAlgebraic(ComplexTrigonometric trygonometric)
        {
            double re = trygonometric.modulus * Math.Cos(trygonometric.arc);
            double im = trygonometric.modulus * Math.Sin(trygonometric.arc);

            return new ComplexAlgebraic(re, im, trygonometric.root);
        }

        /// <summary>
        /// Performs roots calculations for the given list of input complex numbers.
        /// </summary>
        /// <param name="inputs">List of input numbers.</param>
        /// <param name="threadCount">Number of threads to be used for calculations (min. 1, max. 64, default 1).</param>
        /// <param name="lib">Library type that should be used for the main part of the calculation process.</param>
        /// <param name="loggerTextBox">Reference to logging console.</param>
        /// <returns>Presentation of all results corresponding to the given input list.</returns>
        public static ResultsPresentation calculateRoots(List<ComplexAlgebraic> inputs, int threadCount, LibraryType lib, RichTextBox loggerTextBox)
        {
            //set default thread count if it's out of range
            if (threadCount < 1 || threadCount > 64)
                threadCount = 1;

            //create empty list of jobs
            //for each thread one job will be created
            List<CalculationJob> jobs = new List<CalculationJob>();

            //number of inputs passed to one job
            long inputsPerJob = inputs.LongCount() / threadCount;

            //distribute inputs to all jobs equaly
            //the remaining inputs are passed to the last job
            IEnumerator inputsEnumerator = inputs.GetEnumerator();
            for (int i = 0; i < threadCount; i++)
            {
                CalculationJob job = new CalculationJob();
                jobs.Add(job);

                //pass the remaining inputs to the las job
                if (i == threadCount - 1)
                {
                    while (inputsEnumerator.MoveNext())
                    {
                        jobs.Last().inputs.Add((ComplexAlgebraic)inputsEnumerator.Current);
                    }
                }

                //set list of inputs for current job
                for (long j = 0; j < inputsPerJob && inputsEnumerator.MoveNext(); j++)
                {
                    jobs.ElementAt(i).inputs.Add((ComplexAlgebraic)inputsEnumerator.Current);
                }
            }

            //define the calculation task for each thread
            jobs.ForEach(job =>
            {
                job.thread = new Thread(() =>
                {
                    job.inputs.ForEach(i =>
                    {
                        //unsafe - pointers are in use
                        unsafe
                        {
                            //presentation of results fora single input
                            SingleResultPresentation resultPresentation = new SingleResultPresentation();

                            //stores real and imaginary part of each resulting number, so the size is 2 * root
                            double[] results = new double[i.root * 2];

                            //fixed - prevent GC from removing the pointer to array
                            fixed (double* resultsPtr = &results[0])
                            {
                                //convert the input to its trigonometric representation
                                ComplexTrigonometric inputTrig = toTrygonometric(i);

                                //perform calculation using the chosen library
                                if (LibraryType.ASM.Equals(lib))
                                    calculateRootsAsm(inputTrig.modulus, inputTrig.arc, inputTrig.root, resultsPtr);
                                else
                                    calculateRootsCpp(inputTrig.modulus, inputTrig.arc, inputTrig.root, resultsPtr);

                                //convert results to desired representation
                                resultPresentation.input = i;
                                for (int j = 0; j < i.root * 2 - 1; j += 2)
                                    resultPresentation.results.Add(new ComplexRootResult(
                                        Math.Round(resultsPtr[j], 6),
                                        Math.Round(resultsPtr[j + 1], 6)));

                                //add results for the current number to the job's result list
                                job.resultPresenations.Add(resultPresentation);
                            }
                        }
                    });
                });
            });

            loggerTextBox.AppendText("Worker threads have benn initialized.\n");
            loggerTextBox.AppendText("Pleas wait... Results are beeing calculated...\n");

            //start calculation timer
            Stopwatch calculationTimeWatch = new Stopwatch();
            calculationTimeWatch.Start();

            //start all threads
            jobs.ForEach(job => job.thread.Start());

            //synchronize threads
            jobs.ForEach(job => job.thread.Join());

            //get calculation time
            calculationTimeWatch.Stop();
            double calculationDurationMS = ((double)calculationTimeWatch.ElapsedTicks) / (Stopwatch.Frequency) * 1000;

            loggerTextBox.AppendText("Results have been calculated successfully.\n");
            loggerTextBox.AppendText("Calculation duration (ms): " + calculationDurationMS + "\n");

            //join results from each job to one list      
            List<SingleResultPresentation> resultPresentations = new List<SingleResultPresentation>();
            jobs.ForEach(job => resultPresentations.AddRange(job.resultPresenations));

            return new ResultsPresentation(resultPresentations, calculationDurationMS);
        }

    }
}
