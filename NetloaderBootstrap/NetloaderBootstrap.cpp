#include <Windows.h>
#include <mscoree.h>

#define UM_DLL WM_USER
#define UM_CLASS WM_USER + 1
#define UM_METHOD WM_USER + 2
#define UM_ARG WM_USER + 3

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
	LPCWSTR arg = NULL;

	while (dll == NULL || class_name == NULL || method == NULL || arg == NULL)
	{
		if (PeekMessage(&msg, (HWND)-1, UM_DLL, UM_ARG, PM_REMOVE))
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
			case UM_ARG:
				arg = (LPCWSTR)msg.lParam;
				break;
			}
		}
	}

	MessageBox(NULL, dll, L"DLL", MB_OK);
	MessageBox(NULL, class_name, L"Class", MB_OK);
	MessageBox(NULL, method, L"Method", MB_OK);
	MessageBox(NULL, arg, L"Argument", MB_OK);

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
		dll,
		class_name,
		method,
		arg,
		&result
	);
}
