/*
Autor: Micha³ Rzepka
Rodzaj studiów: SSI
Kierunek: Informatyka
Semestr: 5
Grupa dziekañska: 1
Sekcja lab: 2
Przedmiot: Jêzyki Asemblerowe
Email: michrze558@student.polsl.pl
Temat projektu: Wyznaczanie pierwiastków liczb zespolonych
Data oddania projektu: 10-02-2020
*/

/* CHANGELOG
 * 13-11-2019 init project
 * 13-11-2019 add c++ library project
 * 13-11-2919 implement c++ library
 * 30-12-2019 refactor c++ library, initialize asm project
 * 10-02-2020 add documentation
 */

#include "stdafx.h"
#include "ComplexRootLibCpp.h"

#include <cmath>
#include <iostream>
#include <iomanip>

//PI approximation
#define PI 3.14159265358979323846

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
COMPLEXROOTLIBCPP_API void calculateRootsCpp(double modulus, double arc, int n, double* results) {
	
	//calculate size of result array
	const int resultsCount = 2 * n;

	//calculate the n-th root of the modulus
	const double modulusRoot = pow(modulus, 1.0 / n);

	//indicates the k-th of n possible results (k = 0, 1, ..., n - 1)
	int k = 0;

	//argument passed to the trygonometric functions
	double trygArgument;
	
	//calculate all results for the given number according to the formula (see documentation)
	for (int i = 0; i < resultsCount - 1; i += 2) {

		trygArgument = (arc + 2 * k * PI) / n;

		//calculate the real part of the k-th result
		results[i] = modulusRoot * cos(trygArgument);

		//calculate the imaginary part of the k-th result
		results[i + 1] = modulusRoot * sin(trygArgument);
		k++;
	}

}