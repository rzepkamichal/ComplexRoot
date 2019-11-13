// ComplexRootLibC.cpp : Defines the exported functions for the DLL application.
//

#include "stdafx.h"
#include "ComplexRootLibC.h"

#include <cmath>

#define PI (4 * atan(1))

COMPLEXROOTLIBC_API double * calculateRoots(double modulus, double arc, int n)
{
	const int RESULTS_ARRAY_SIZE = n;
	double* results = new double[RESULTS_ARRAY_SIZE];

	//calculate the n-th root of the modulus
	const double modulusRoot = pow(modulus, 1.0 / n);

	//indicates the k-th of n possible results (k = 0, 1, ..., n - 1)
	int k = 0;

	//argument passed to the trygonometric functions
	double trygArgument;

	for (int i = 0; i < RESULTS_ARRAY_SIZE; i += 2) {
		
		trygArgument = (arc + 2 * k * PI) / n;
		
		//calculate the real part of the k-th result
		results[i] = modulusRoot * cos(trygArgument);

		//calculate the imaginary part of the k-th result
		results[i + 1] = modulusRoot * sin(trygArgument);
		
		k++;
	}
	
	return results;
}
