// POConEcoQoSInCpp.cpp : This file contains the 'main' function. Program execution begins and ends there.
//

#include <iostream>
#include <windows.h>

int main()
{
    std::cout << "Hello World!\n";
    HANDLE process = GetCurrentProcess();//OpenProcess(PROCESS_ALL_ACCESS, FALSE, 15336);

    PROCESS_POWER_THROTTLING_STATE PowerThrottling;
    RtlZeroMemory(&PowerThrottling, sizeof(PowerThrottling));

    BOOL rs = GetProcessInformation(process, ProcessPowerThrottling, &PowerThrottling, sizeof(PowerThrottling));

    std::cout << "Result: " << rs << std::endl;

    std::cout << "PowerThrottling.Version: " << PowerThrottling.Version << std::endl;
    std::cout << "PowerThrottling.ControlMask: " << PowerThrottling.ControlMask << std::endl;
    std::cout << "PowerThrottling.StateMask: " << PowerThrottling.StateMask << std::endl;

    MEMORY_PRIORITY_INFORMATION MemPrio;
    ZeroMemory(&MemPrio, sizeof(MemPrio));

    rs = GetProcessInformation(process, ProcessMemoryPriority, &MemPrio, sizeof(MemPrio));

    std::cout << "Result: " << rs << std::endl;

    std::cout << "MemPrio.Version: " << MemPrio.MemoryPriority << std::endl;
}