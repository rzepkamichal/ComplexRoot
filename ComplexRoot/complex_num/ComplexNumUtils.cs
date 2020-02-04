using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ComplexRoot.complex_num
{
    class ComplexNumUtils
    {

        [DllImport("ComplexRootLibCpp.dll")]
        static extern unsafe void calculateRootsCpp(double modulus, double arc, int n, double[] results);

        [DllImport("ComplexRootLibAsm.dll")]
        static extern unsafe double calculateRootsAsm(double modulus, double arc, int n, double[] results);

        public static ComplexNumTrygonometric toTrygonometric(ComplexNumAlgebraic algebraic)
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
            else if(algebraic.re < 0 && algebraic.im < 0)
            {
                arc = Math.Atan(algebraic.im / algebraic.re) - Math.PI;
            }

            return new ComplexNumTrygonometric(modulus, arc, algebraic.root);
        }

        public static List<ComplexRootResultPresentation> calculateRoots(List<ComplexNumTrygonometric> inputs)
        {
            ComplexNumAlgebraic input = new ComplexNumAlgebraic { re = 16.34, im = -12.2, root = 4 };
            ComplexNumTrygonometric inputTryg = ComplexNumUtils.toTrygonometric(input);

            double[] data = new double[8];
      
            calculateRootsCpp(inputTryg.modulus, inputTryg.arc, inputTryg.root, data);

            ComplexRootResultPresentation resultPresentation = new ComplexRootResultPresentation();

            resultPresentation.input = input;
            resultPresentation.results = new List<ComplexRootResult>();

            for (int i = 0; i < data.Length - 1; i += 2)
                resultPresentation.results.Add(new ComplexRootResult(data[i], data[i + 1]));

            List<ComplexRootResultPresentation> resultPresentations = new List<ComplexRootResultPresentation>();
            resultPresentations.Add(resultPresentation);

            return resultPresentations;
        }
        
    }
}
