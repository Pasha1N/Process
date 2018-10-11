using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Process.ViewModels
{
    public class ViewModelProcesses
    {
        private const string dllName = @"Kernel32.dll";
        private int invalidValue = -1;
        private ICollection<IntPtr> processDescriptors = new List<IntPtr>();



        //IntPtr intPtr = MethodsFromUnmanagedCode.CreateToolhelp32Snapshot(0x00000002, 0);


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
                        processDescriptors.Add(handle);
                    }
                    while (MethodsFromUnmanagedCode.NextProcess(handle, ref processEntry));
                    
                }
            }



        }
    }
}