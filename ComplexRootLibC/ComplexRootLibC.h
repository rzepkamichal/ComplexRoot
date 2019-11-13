#pragma once


#ifdef COMPLEXROOTLIBC_EXPORTS
#define COMPLEXROOTLIBC_API __declspec(dllexport)
#else
#define COMPLEXROOTLIBC_API __declspec(dllimport)
#endif

extern "C" COMPLEXROOTLIBC_API double* calculateRoots(double modulus, double arc, int n);
