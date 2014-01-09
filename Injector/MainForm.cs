using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;

namespace Injector
{
    public partial class MainForm : Form
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public MainForm()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            EnumProcesses();
        }

        /// <summary>
        /// Handles the clicke events of all buttons.
        /// </summary>
        void OnButtonClick(object sender, EventArgs e)
        {
            if (sender == button_refresh) // [Refresh]
            {
                EnumProcesses();
            }
            else if (sender == button_dllpath) // [...] Dll
            {
                //show file browser to provide the dll path
                using(OpenFileDialog ofd = new OpenFileDialog())
                {
                    ofd.Filter = "Dynamic Link Library|*.dll";

                    if(ofd.ShowDialog() == DialogResult.OK)
                    {
                        textBox_dllpath.Text = ofd.FileName;
                    }
                }
            }
            else if (sender == button_injectload)
            {
                if(tabControl_mode.SelectedTab == tabPage_inject) // [Inject]
                {
                    //inject the dll into the process
                    Process target_proc = listView_procs.SelectedItems[0].Tag as Process;

                    MessageBox.Show
                    (
                        (target_proc != null && InjectDll(target_proc, textBox_dllpath.Text)) ?
                        "Success!" :
                        "Failed!"
                    ); 
                }
                else // [Load]
                {
                    MessageBox.Show
                    (
                        LoadDll(textBox_exepath.Text, textBox_dllpath.Text) ?
                        "Success!" :
                        "Failed!"
                    ); 
                }            
            }
            else if(sender == button_exepath) // [...] Executable
            {
                //show file browser to provide the exe path
                using (OpenFileDialog ofd = new OpenFileDialog())
                {
                    ofd.Filter = "Executable|*.exe";

                    if (ofd.ShowDialog() == DialogResult.OK)
                    {
                        textBox_exepath.Text = ofd.FileName;
                    }
                }
            }
        }

        /// <summary>
        /// Handles the selection of the target process and the DLL path.
        /// </summary>
        void OnDataSelection(object sender, EventArgs e)
        {
            //Enable the inject button if both process/file and dll path are provided. Otherwise disable the button.
            button_injectload.Enabled =
                (textBox_dllpath.Text.EndsWith(".dll", true, System.Globalization.CultureInfo.CurrentCulture) &&
                File.Exists(textBox_dllpath.Text)) &&
                (tabControl_mode.SelectedTab == tabPage_inject ?
                    (listView_procs.SelectedItems.Count == 1):
                    File.Exists(textBox_exepath.Text));          
        }

        /// <summary>
        /// Enumaretas all running 32-Bit process and displays them in the list.
        /// </summary>
        void EnumProcesses()
        {
            //Clear the list
            listView_procs.Items.Clear();
            imageList_icons.Images.Clear();

            Process[] procs = Process.GetProcesses();

            foreach(Process proc in procs)
            {
                //skip 64-Bit processes
                if (!IsProcess32Bit(proc)) continue;
                
                //create new list entry
                ListViewItem lvi = new ListViewItem();
                listView_procs.Items.Add(lvi);

                lvi.Text = proc.ProcessName;
                //tag the process to the list entry
                lvi.Tag = proc;

                //extract the process icon
                try
                {
                    //get the module path
                    string module_path = proc.MainModule.FileName;

                    //check if the icon is already loaded
                    if(!imageList_icons.Images.ContainsKey(module_path))
                    {
                        imageList_icons.Images.Add(module_path, Icon.ExtractAssociatedIcon(module_path));
                    }

                    //associate list entry with the icon
                    lvi.ImageKey = module_path;
                }
                catch { }
            }
        }

        /// <summary>
        /// Injects a native Dll into a 32-Bit process. ONLY WORKS OF COMPILED AS x86!
        /// </summary>
        /// <param name="targetProcess">The target process.</param>
        /// <param name="dllPath">The path to the native Dll.</param>
        bool InjectDll(Process targetProcess, string dllPath)
        {
            try
            {
                //STEP 1
                //============================================
                //find the address of the LoadLibraryA function
                //therefore, the dll containing this function must be loaded (temporarily)
                //============================================

                IntPtr kernel32_dll_handle = Win32.LoadLibraryA("kernel32.dll"); //load the kernel32.dll
                UIntPtr load_library_a_address = Win32.GetProcAddress(kernel32_dll_handle, "LoadLibraryA"); //find the address of the function
                Win32.FreeLibrary(kernel32_dll_handle); //finally release the kernel32.dll

                //check if the address of LoadLibraryA is found
                if (load_library_a_address == UIntPtr.Zero)
                    return false;

                //STEP 2
                //============================================
                //create a remote thread in the target process which loads the dll using the LoadLibraryA function.
                //therefore, access is needed and the parameter for the LoadLibraryA function must be written to the process' memory
                //============================================

                //get access
                IntPtr process_handle = Win32.OpenProcess
                (
                    Win32.ProcessAccess.AllAccess,
                    false,
                    targetProcess.Id
                );

                if (process_handle == IntPtr.Zero)
                    return false;

                //get the bytes of the dll path
                byte[] dll_name = Encoding.Default.GetBytes(dllPath);

                //allocate memory in the target process in order to store the path
                IntPtr cave_address = Win32.VirtualAllocEx
                (
                    process_handle,
                    IntPtr.Zero, //let the function find a free place in memory for storage
                    (uint)dll_name.Length, //allocate enough bytes for the path
                    Win32.AllocationType.Commit | Win32.AllocationType.Reserve,
                    Win32.MemoryProtection.ReadWrite //full access is desired
                );

                //check if the allocation was successful
                if (cave_address == IntPtr.Zero)
                    return false;

                //now the dll path needs to be written to the allocated memory

                UIntPtr bytes_written;
                if (!Win32.WriteProcessMemory
                (
                    process_handle,
                    cave_address,
                    dll_name,
                    (uint)dll_name.Length,
                    out bytes_written
                ))
                    return false;

                //finally, call the function "LoadLibraryA" from within the process and pass the dll path as parameter
                IntPtr thread_handle = IntPtr.Zero;

                Win32.CreateRemoteThread
                (
                    process_handle,
                    IntPtr.Zero,
                    0u,
                    load_library_a_address, //execute the LoadLibraryA function
                    cave_address, //use the dll_path as parameter
                    0u,
                    out thread_handle
                );

                //if the remote thread was created successfully, return true
                return thread_handle != IntPtr.Zero;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Opens a new process and loads the dll to it.
        /// </summary>
        /// <param name="targetExecutable">The executable to open.</param>
        /// <param name="dllPath">The Dll to inject.</param>
        bool LoadDll(string targetExecutable, string dllPath)
        {
            try
            {
                Win32.STARTUPINFO startup_info = new Win32.STARTUPINFO();
                Win32.PROCESS_INFORMATION proc_info;

                //create suspended process!                
                if (!Win32.CreateProcess
                    (
                        targetExecutable,
                        string.Empty,
                        IntPtr.Zero,
                        IntPtr.Zero,
                        false,
                        Win32.CreationFlags.CREATE_SUSPENDED,
                        IntPtr.Zero,
                        Path.GetDirectoryName(targetExecutable),
                        ref startup_info,
                        out proc_info
                    ))
                    return false;

                //inject dll
                if (!InjectDll(Process.GetProcessById(proc_info.dwProcessId), dllPath))
                    return false;

                //resume suspended process-thread
                Win32.ResumeThread(proc_info.hThread);

                return true;
            }
            catch { return false; }
        }

        /// <summary>
        /// Returns true if a specific process is a 32-bit process.
        /// </summary>
        /// <param name="process">The process to check.</param>
        bool IsProcess32Bit(Process process)
        {
            //if the current system is 32-bit, return true
            if (Environment.GetEnvironmentVariable("PROCESSOR_ARCHITECTURE", EnvironmentVariableTarget.Machine) == "x86")
                return true;

            //if the current system is 64-bit, check whether the process is emulated by WOW64
            bool is_emulated = false;
            try
            {
                Win32.IsWow64Process(process.Handle, out is_emulated);
            }
            catch { }

            //if emulated, the process is a 32-bit process
            return is_emulated;
        }

        /// <summary>
        /// Handles the DragDrop and DragOver events of the textboxes.
        /// </summary>
        void OnDragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);

                if (files.Length == 1 &&
                    files[0].EndsWith(sender == textBox_dllpath ? ".dll" : ".exe", true, System.Globalization.CultureInfo.CurrentCulture))
                {
                    //KEYSTATE BITS
                    /*
                     * 1:   MOUSE_LEFT
                     * 2:   MOUSE_RIGHT
                     * 4:   SHIFT
                     * 8:   CTRL
                     * 16:  MOUSE_CENTER
                     * 32:  ALT
                     * 
                     */
                    if ((e.KeyState & 1) == 1) //left mouse down ^= DragOver
                    {
                        e.Effect = DragDropEffects.Link;
                    }
                    else //left mouse up ^= DragDrop
                    {
                        (sender as TextBox).Text = files[0];
                    }
                }
            }
        }

        /// <summary>
        /// Handles the change of the tabs.
        /// </summary>
        void OnModeChanged(object sender, EventArgs e)
        {
            button_injectload.Text = tabControl_mode.SelectedTab == tabPage_inject ?
                "Inject" :
                "Load";

            //enable/disable button
            OnDataSelection(null, EventArgs.Empty);
        }

        /// <summary>
        /// Focusses the ListView if the mouse enters.
        /// </summary>
        void OnMouseEnterListview(object sender, EventArgs e)
        {
            listView_procs.Focus();
        }
    }
}
