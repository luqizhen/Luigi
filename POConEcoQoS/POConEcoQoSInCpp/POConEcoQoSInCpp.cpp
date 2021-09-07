// POConEcoQoSInCpp.cpp : This file contains the 'main' function. Program execution begins and ends there.
//

#include <iostream>
#include <windows.h>
#include <strsafe.h>

void ShowError()
{
    // Retrieve the system error message for the last-error code

    LPVOID lpMsgBuf;
    DWORD dw = GetLastError();

    FormatMessage(
        FORMAT_MESSAGE_ALLOCATE_BUFFER |
        FORMAT_MESSAGE_FROM_SYSTEM |
        FORMAT_MESSAGE_IGNORE_INSERTS,
        NULL,
        dw,
        MAKELANGID(LANG_NEUTRAL, SUBLANG_DEFAULT),
        (LPTSTR)&lpMsgBuf,
        0, NULL);

    // Display the error message and exit the process
    std::wcout << (LPTSTR)lpMsgBuf << std::endl;

    LocalFree(lpMsgBuf);
    ExitProcess(dw);
}

int main()
{
    PROCESS_POWER_THROTTLING_STATE PowerThrottling;
    RtlZeroMemory(&PowerThrottling, sizeof(PowerThrottling));

    PowerThrottling.Version = PROCESS_POWER_THROTTLING_CURRENT_VERSION;
    PowerThrottling.ControlMask = PROCESS_POWER_THROTTLING_EXECUTION_SPEED;
    PowerThrottling.StateMask = PROCESS_POWER_THROTTLING_EXECUTION_SPEED;

    BOOL set_rs = SetProcessInformation(
        GetCurrentProcess(),
        ProcessPowerThrottling,
        &PowerThrottling,
        sizeof(PowerThrottling));

    if (set_rs == 0) {
        std::cout << "Error when set: ";
        ShowError();
    }
    else {
        std::cout << "Set successful." << std::endl;
    }

    BOOL get_rs = GetProcessInformation(
        GetCurrentProcess(),
        ProcessPowerThrottling,
        &PowerThrottling,
        sizeof(PowerThrottling));

    if (get_rs == 0) {
        std::cout << "Error when get: ";
        ShowError();
    }
    else {
        std::cout << "PowerThrottling.Version: " << PowerThrottling.Version << std::endl;
        std::cout << "PowerThrottling.ControlMask: " << PowerThrottling.ControlMask << std::endl;
        std::cout << "PowerThrottling.StateMask: " << PowerThrottling.StateMask << std::endl;
    }



}