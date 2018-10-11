using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Process
{
    internal class MethodsFromUnmanagedCode
    {
        private const string dllName = @"Kernel32.dll";
        private int invalidValue = -1;
        private ICollection<IntPtr> processDescriptors = new List<IntPtr>();

        [DllImport(dllName)]

        public static extern IntPtr CreateToolhelp32Snapshot(int TH32CS_SNAPPROCESS, int idProcess);



        public void GetProcessHandle()
        {
            int invalidValue = -1;
            IntPtr handle = CreateToolhelp32Snapshot(0x00000002, 0);
            if (handle != (IntPtr)invalidValue)
            {
                


            }



        }



    }
}