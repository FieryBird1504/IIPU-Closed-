#include <stdlib.h>
#include <Windows.h>
#include <Powrprof.h>

#pragma comment(lib, "Powrprof.lib")

extern "C"
{
	__declspec(dllexport) int StatusCheck()
	{
	}

	__declspec(dllexport) int RemainTime()
	{
	}

	__declspec(dllexport) int RemainPercent()
	{
	}

	__declspec(dllexport) void SetTimeout(int new_timeout)
	{
	}

	__declspec(dllexport) int GetTimeout()
	{
	}
}