using System;
using System.Runtime.InteropServices;

namespace Process
{
    internal class MethodsFromUnmanagedCode
    {
        private const string dllName = @"Kernel32.dll";
        int idProcess = 0;

        [DllImport(dllName)]

        public static extern IntPtr CreateToolhelp32Snapshot(int TH32CS_SNAPPROCESS, int idProcess);
    }
}