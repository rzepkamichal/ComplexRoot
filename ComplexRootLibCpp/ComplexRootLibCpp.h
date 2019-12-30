#pragma once

#ifdef COMPLEXROOTLIBCPP_EXPORTS
#define COMPLEXROOTLIBCPP_API __declspec(dllexport)
#else
#define COMPLEXROOTLIBCPP_API __declspec(dllimport)
#endif

extern "C" COMPLEXROOTLIBCPP_API double* calculateRoots(double modulus, double arc, int n);
