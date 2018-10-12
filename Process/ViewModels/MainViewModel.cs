using Process;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Processes.ViewModels
{
    public class MainViewModel
    {
        private const string dllName = @"Kernel32.dll";
        private int invalidValue = -1;
        private ICollection<ProcessViewModel> processDescriptors = new List<ProcessViewModel>();

        public MainViewModel()
        {
            GetProcessHandle();
        }

        public IEnumerable<ProcessViewModel> Processes => processDescriptors; 

        public void GetProcessHandle()
        {
            int invalidValue = -1;
            IntPtr handle = MethodsFromUnmanagedCode.CreateToolhelp32Snapshot(0x00000002, 0);
            ProcessEntry processEntry = new ProcessEntry();
            processEntry.DwSize = Marshal.SizeOf<ProcessEntry>();

            if (handle != (IntPtr)invalidValue)
            {
                if (MethodsFromUnmanagedCode.FirstProcess(handle, ref processEntry))
                {
                    do
                    {
                        Models.Process proces = new Models.Process(processEntry.ExeFile,processEntry.ProcessID);
                        ProcessViewModel processViewModel = new ProcessViewModel(proces);
                        processDescriptors.Add(processViewModel);
                    }
                    while (MethodsFromUnmanagedCode.NextProcess(handle, ref processEntry));
                }
            }
        }
    }
}