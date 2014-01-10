#include <Windows.h>
#include <dwmapi.h>

#include "detours.h"

#pragma comment(lib, "detours.lib") //include the detours library
#pragma comment(lib, "dwmapi.lib")

BOOL(WINAPI *pGetMessage)(LPMSG lpMsg, HWND hWnd, UINT wMsgFilterMin, UINT wMsgFilterMax) = GetMessage;
BOOL WINAPI MyGetMessage(LPMSG lpMsg, HWND hWnd, UINT wMsgFilterMin, UINT wMsgFilterMax);

//The entrypoint of the DLL.
INT APIENTRY DllMain(HMODULE hDLL, DWORD Reason, LPVOID Reserved)
{
	switch (Reason)
	{
	case DLL_PROCESS_ATTACH:
		MessageBox(NULL, L"Injected >:D", L"Success", MB_OK | MB_ICONINFORMATION);
		//if the dll is being attached, detour the functions
		DisableThreadLibraryCalls(hDLL);

		// detour ShowWindow
		DetourTransactionBegin();
		DetourUpdateThread(GetCurrentThread());
		DetourAttach(&(PVOID&)pGetMessage, MyGetMessage);
		DetourTransactionCommit();		
		break;
	case DLL_PROCESS_DETACH:
		//if the dll is being detached, remove the previously installed detours

		//remove ShowWindow detour
		DetourTransactionBegin();
		DetourUpdateThread(GetCurrentThread());
		DetourDetach(&(PVOID&)pGetMessage, MyGetMessage);
		DetourTransactionCommit();
		break;
	case DLL_THREAD_ATTACH:
	case DLL_THREAD_DETACH:
		break;
	}

	return TRUE;
}

bool initialized = false;

BOOL WINAPI MyGetMessage(LPMSG lpMsg, HWND hWnd, UINT wMsgFilterMin, UINT wMsgFilterMax)
{
	if (!initialized)
	{
		MARGINS margins;
		margins.cxLeftWidth = 20;
		margins.cxRightWidth = 20;
		margins.cyBottomHeight = 20;
		margins.cyTopHeight = 20;
		if (!SUCCEEDED(DwmExtendFrameIntoClientArea(hWnd, &margins)))
		{
			//wchar_t error[256];

			//FormatMessage(FORMAT_MESSAGE_FROM_SYSTEM,
			//	NULL,
			//	GetLastError(),
			//	MAKELANGID(LANG_NEUTRAL, SUBLANG_DEFAULT),
			//	error,
			//	255,
			//	NULL);

			//MessageBox(NULL, error, L"Error", MB_OK | MB_ICONERROR);

		}
		else
		{
		
		MessageBox(NULL, L"Extended Frame.", L"Success", MB_OK | MB_ICONINFORMATION);
		initialized = true;
		}
	}
	
	return pGetMessage(lpMsg, hWnd, wMsgFilterMin, wMsgFilterMax);
}
