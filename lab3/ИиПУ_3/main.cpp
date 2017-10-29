#include <stdlib.h>
#include <Windows.h>
#include <Powrprof.h>

#pragma comment(lib, "Powrprof.lib")

extern "C"
{
	__declspec(dllexport) int StatusCheck()
	{
		SYSTEM_POWER_STATUS system_power_status;
		ZeroMemory(&system_power_status, sizeof(SYSTEM_POWER_STATUS));
		GetSystemPowerStatus(&system_power_status);
		return system_power_status.ACLineStatus;
	}

	__declspec(dllexport) int RemainTime()
	{
		SYSTEM_POWER_STATUS system_power_status;
		ZeroMemory(&system_power_status, sizeof(SYSTEM_POWER_STATUS));
		GetSystemPowerStatus(&system_power_status);
		return system_power_status.BatteryLifeTime;
	}

	__declspec(dllexport) int RemainPercent()
	{
		SYSTEM_POWER_STATUS system_power_status;
		ZeroMemory(&system_power_status, sizeof(SYSTEM_POWER_STATUS));
		GetSystemPowerStatus(&system_power_status);
		return (system_power_status.BatteryLifePercent);
	}

	__declspec(dllexport) void SetTimeout(int new_timeout)
	{
		SYSTEM_POWER_POLICY system_power_policy;
		CallNtPowerInformation(SystemPowerPolicyDc, NULL, 0, &system_power_policy, sizeof(system_power_policy));
		system_power_policy.VideoTimeout = new_timeout;
		CallNtPowerInformation(SystemPowerPolicyDc, &system_power_policy, sizeof(system_power_policy), NULL, 0);
	}

	__declspec(dllexport) int GetTimeout()
	{
		SYSTEM_POWER_POLICY system_power_policy;
		CallNtPowerInformation(SystemPowerPolicyDc, NULL, 0, &system_power_policy, sizeof(system_power_policy));
		return system_power_policy.VideoTimeout;
	}
}