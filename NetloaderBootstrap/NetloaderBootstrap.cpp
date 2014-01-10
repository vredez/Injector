#include <Windows.h>
#include <mscoree.h>

#define UM_DLL WM_USER
#define UM_CLASS WM_USER + 1
#define UM_METHOD WM_USER + 2

#pragma comment(lib, "mscoree.lib")

void LoadNetRuntime();

INT APIENTRY DllMain(HMODULE hDLL, DWORD Reason, LPVOID Reserved)
{
	switch (Reason)
	{
	case DLL_PROCESS_ATTACH:
		LoadNetRuntime();
		break;
	}

	return TRUE;
}

void LoadNetRuntime()
{
	//wait for the dll name of the netintruder
	MSG msg;

	LPCWSTR dll = NULL;
	LPCWSTR class_name = NULL;
	LPCWSTR method = NULL;
	
	while (dll == NULL && class_name == NULL && method == NULL)
	{
		if (PeekMessage(&msg, (HWND)-1, UM_DLL, UM_METHOD, PM_REMOVE))
		{
			switch (msg.message)
			{
			case UM_DLL:
				dll = (LPCWSTR)msg.lParam;
				break;
			case UM_CLASS:
				class_name = (LPCWSTR)msg.lParam;
				break;
			case UM_METHOD:
				method = (LPCWSTR)msg.lParam;
				break;
			}
		}
	}

	ICLRRuntimeHost *clr_host = NULL;
	
	CorBindToRuntimeEx
	(
		NULL,
		L"wks",
		0,
		CLSID_CLRRuntimeHost,
		IID_ICLRRuntimeHost,
		(PVOID*)&clr_host
	);

	clr_host->Start();

	DWORD result;

	clr_host->ExecuteInDefaultAppDomain
	(
		(LPCWSTR)msg.lParam,
		L"NetIntruder.Intruder",
		L"Intrude",
		L"NetloaderBootstrap",
		&result
	);
}
