#include <Windows.h>
#include "detours.h"

#pragma comment(lib, "detours.lib") //include the detours library


//BOOL(WINAPI *pGetMessage)(LPMSG lpMsg, HWND hWnd, UINT wMsgFilterMin, UINT wMsgFilterMax)

//The entrypoint of the DLL.
INT APIENTRY DllMain(HMODULE hDLL, DWORD Reason, LPVOID Reserved)
{
	switch (Reason)
	{
	case DLL_PROCESS_ATTACH:
		MessageBox(NULL, L"Injected >:D", L"Success", MB_OK | MB_ICONINFORMATION);
		////if the dll is being attached, detour the functions
		//DisableThreadLibraryCalls(hDLL);

		//// detour ShowWindow
		//DetourTransactionBegin();
		//DetourUpdateThread(GetCurrentThread());
		//DetourAttach(&(PVOID&)pDrawTextEx, MyDrawTextEx);
		//DetourTransactionCommit();		
		break;
	case DLL_PROCESS_DETACH:
		////if the dll is being detached, remove the previously installed detours

		////remove ShowWindow detour
		//DetourTransactionBegin();
		//DetourUpdateThread(GetCurrentThread());
		//DetourDetach(&(PVOID&)pDrawTextEx, MyDrawTextEx);
		//DetourTransactionCommit();
		break;
	case DLL_THREAD_ATTACH:
	case DLL_THREAD_DETACH:
		break;
	}

	return TRUE;
}


//int WINAPI MyDrawTextEx(HDC hDC, LPWSTR lpchText, int nCount, LPRECT lpRect, UINT uFormat, LPDRAWTEXTPARAMS lpDTParams)
//{
//	return pDrawTextEx(hDC, lpchText, 1, lpRect, uFormat, lpDTParams);
//}
