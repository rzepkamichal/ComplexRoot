/*
Autor: Micha� Rzepka
Rodzaj studi�w: SSI
Kierunek: Informatyka
Semestr: 5
Grupa dzieka�ska: 1
Sekcja lab: 2
Przedmiot: J�zyki Asemblerowe
Email: michrze558@student.polsl.pl
Temat projektu: Wyznaczanie pierwiastk�w liczb zespolonych
Data oddania projektu: 10-02-2020
*/

#pragma once

#ifdef COMPLEXROOTLIBCPP_EXPORTS
#define COMPLEXROOTLIBCPP_API __declspec(dllexport)
#else
#define COMPLEXROOTLIBCPP_API __declspec(dllimport)
#endif

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
extern "C" COMPLEXROOTLIBCPP_API void calculateRootsCpp(double modulus, double arc, int n, double* results);
