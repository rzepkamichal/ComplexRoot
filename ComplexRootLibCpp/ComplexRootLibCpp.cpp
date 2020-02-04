#include "stdafx.h"
#include "ComplexRootLibCpp.h"

#include <cmath>
#include <iostream>
#include <iomanip>

#define PI 3.14159265358979323846

COMPLEXROOTLIBCPP_API void calculateRootsCpp(double modulus, double arc, int n, double* results) {
	
	const int resultsCount = 2 * n;
	double pi = 3.14159265358979323846;

	//calculate the n-th root of the modulus
	const double modulusRoot = pow(modulus, 1.0 / n);

	//indicates the k-th of n possible results (k = 0, 1, ..., n - 1)
	int k = 0;

	//argument passed to the trygonometric functions
	double trygArgument;
	
	for (int i = 0; i < resultsCount - 1; i += 2) {

		trygArgument = (arc + 2 * k * pi) / n;

		//calculate the real part of the k-th result
		results[i] = modulusRoot * cos(trygArgument);

		//calculate the imaginary part of the k-th result
		results[i + 1] = modulusRoot * sin(trygArgument);
		k++;
	}

}