#include <Windows.h>
#include <mscoree.h>

#pragma comment(lib, "mscoree.lib")

#define UM_DLL WM_USER
#define UM_CLASS WM_USER + 1
#define UM_METHOD WM_USER + 2
#define UM_ARG WM_USER + 3

struct NET_DLL_INFO
{
	LPARAM lpDllPath;
	LPARAM lpClassName;
	LPARAM lpMethod;
	LPARAM lpArg;
};

typedef NET_DLL_INFO *LP_NET_DLL_INFO;

DWORD WINAPI LoadNetRuntime(LPVOID param);
void ReceiveDllInfo(LP_NET_DLL_INFO lpDllInfo);

INT APIENTRY DllMain(HMODULE hDLL, DWORD Reason, LPVOID Reserved)
{
	if (Reason == DLL_PROCESS_ATTACH)
	{
		LP_NET_DLL_INFO lpDllInfo = new NET_DLL_INFO;
		ReceiveDllInfo(lpDllInfo);
		CreateThread(NULL, 0, LoadNetRuntime, (LPVOID)lpDllInfo, 0, NULL);
	}

	return TRUE;
}

void ReceiveDllInfo(LP_NET_DLL_INFO lpDllInfo)
{
	MSG msg;
	
	ZeroMemory(lpDllInfo, sizeof(NET_DLL_INFO));

	//message loop
	while (lpDllInfo->lpDllPath == NULL || lpDllInfo->lpClassName == NULL || lpDllInfo->lpMethod == NULL || lpDllInfo->lpArg == NULL)
	{
		if (GetMessage(&msg, (HWND)-1, UM_DLL, UM_ARG))
		{
			switch (msg.message)
			{
			case UM_DLL:
				lpDllInfo->lpDllPath = msg.lParam;
				break;
			case UM_CLASS:
				lpDllInfo->lpClassName = msg.lParam;
				break;
			case UM_METHOD:
				lpDllInfo->lpMethod = msg.lParam;
				break;
			case UM_ARG:
				lpDllInfo->lpArg = msg.lParam;
				break;
			}
		}
	}
}

DWORD WINAPI LoadNetRuntime(LPVOID param)
{
	LP_NET_DLL_INFO lpDllInfo = (LP_NET_DLL_INFO)param;

	ICLRRuntimeHost *clr_host = NULL;

	CorBindToRuntimeEx
	(
		NULL,
		L"wks",
		0,
		CLSID_CLRRuntimeHost,
		IID_ICLRRuntimeHost,
		(LPVOID*)&clr_host
	);

	clr_host->Start();

	DWORD result;

	HRESULT h_result = clr_host->ExecuteInDefaultAppDomain
	(
		(LPCWSTR)lpDllInfo->lpDllPath,
		(LPCWSTR)lpDllInfo->lpClassName,
		(LPCWSTR)lpDllInfo->lpMethod,
		(LPCWSTR)lpDllInfo->lpArg,
		&result
	);

	delete lpDllInfo;

	return 0;
}
