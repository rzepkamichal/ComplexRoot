using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

namespace ComplexRoot.complex_num
{

    public enum LibraryType
    {
        ASM,
        CPP
    }

    class ComplexNumUtils
    {

        [DllImport("ComplexRootLibCpp.dll", CallingConvention = CallingConvention.StdCall)]
        public unsafe static extern void calculateRootsCpp(double modulus, double arc, int n, double* results);

        [DllImport("ComplexRootLibAsm.dll", CallingConvention = CallingConvention.StdCall)]
        public unsafe static extern void calculateRootsAsm(double modulus, double arc, int n, double* results);

        [DllImport("kernel32", SetLastError = true)]
        private static extern IntPtr LoadLibrary(string lpFileName);

        public static bool areLibrariesPresent()
        {
            return LoadLibrary("ComplexRootLibCpp.dll") != IntPtr.Zero
                && LoadLibrary("ComplexRootLibAsm.dll") != IntPtr.Zero;
        }

        public static ComplexTrigonometric toTrygonometric(ComplexAlgebraic algebraic)
        {
            double modulus = Math.Sqrt(Math.Pow(algebraic.im, 2.0) + Math.Pow(algebraic.re, 2.0));

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

        public static ComplexAlgebraic toAlgebraic(ComplexTrigonometric trygonometric)
        {
            double re = trygonometric.modulus * Math.Cos(trygonometric.arc);
            double im = trygonometric.modulus * Math.Sin(trygonometric.arc);

            return new ComplexAlgebraic(re, im, trygonometric.root);
        }


        public static ResultsPresentation calculateRoots(List<ComplexAlgebraic> inputs, int threadCount, LibraryType lib, RichTextBox loggerTextBox)
        {
            if (threadCount < 1 || threadCount > 64)
                threadCount = 1;

            //create empty jobs
            List<CalculationJob> jobs = new List<CalculationJob>();
            for (int i = 0; i < threadCount; i++)
            {
                CalculationJob job = new CalculationJob();
                jobs.Add(job);
            }

            //assign possibly equal portions of inputs to each job
            int jobIndex = 0;
            for (int i = 0; i < inputs.Count; i++)
            {
                jobs.ElementAt(jobIndex).inputs.Add(inputs.ElementAt(i));
                jobIndex++;
                jobIndex %= threadCount;
            }


            //define tasks for each thread
            jobs.ForEach(job =>
            {
                job.thread = new Thread(() =>
                {
                    job.inputs.ForEach(i =>
                    {
                        unsafe
                        {
                            SingleResultPresentation resultPresentation = new SingleResultPresentation();

                            double[] results = new double[i.root * 2];
                            fixed (double* resultsPtr = &results[0])
                            {
                                ComplexTrigonometric inputTrig = toTrygonometric(i);

                                //perform calculation
                                if (LibraryType.ASM.Equals(lib))
                                    calculateRootsAsm(inputTrig.modulus, inputTrig.arc, inputTrig.root, resultsPtr);
                                else
                                    calculateRootsCpp(inputTrig.modulus, inputTrig.arc, inputTrig.root, resultsPtr);

                                //convert results and add to the job's resultlist
                                resultPresentation.input = i;
                                for (int j = 0; j < i.root * 2 - 1; j += 2)
                                    resultPresentation.results.Add(new ComplexRootResult(
                                        Math.Round(resultsPtr[j], 6),
                                        Math.Round(resultsPtr[j + 1], 6)));

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

            ////start all threads
            jobs.ForEach(job => job.thread.Start());

            ////synchronize threads
            jobs.ForEach(job => job.thread.Join());

            //get calculation time
            calculationTimeWatch.Stop();
            double calculationDurationMS = ((double)calculationTimeWatch.ElapsedTicks) / (Stopwatch.Frequency) * 1000;

            loggerTextBox.AppendText("Results have been calculated successfully.\n");
            loggerTextBox.AppendText("Calculation duration (ms): " + calculationDurationMS + "\n");

            //join results to one list      
            List<SingleResultPresentation> resultPresentations = new List<SingleResultPresentation>();
            jobs.ForEach(job => resultPresentations.AddRange(job.resultPresenations));

            return new ResultsPresentation(resultPresentations, calculationDurationMS);
        }

    }
}
