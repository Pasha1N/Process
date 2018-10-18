﻿using Processes.Models;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Processes
{
    internal class MethodsFromUnmanagedCode
    {
        private const string dllName = @"Kernel32.dll";

        [DllImport(dllName)]
        public static extern IntPtr CreateToolhelp32Snapshot(int TH32CS_SNAPPROCESS, int idProcess);

        [DllImport(dllName, EntryPoint = "Process32First")]
        public static extern bool FirstProcess(IntPtr handle, ref ProcessEntry @struct);

        [DllImport(dllName, EntryPoint = "Process32Next")]
        public static extern bool NextProcess(IntPtr handle, ref ProcessEntry @struct);

        [DllImport(dllName, SetLastError =true)]
        public static extern bool CreateProcess(string lpApplicationName
            , string lpCommandLine
            , IntPtr lpProcessAttributes
            , IntPtr lpThreadAttributes
            , bool bInheritHandles
            , int dwCreationFlags
            , IntPtr lpEnvironment
            , string lpCurrentDirectory
            , ref Startupinfoa lpStartupInfo
            , ref ProcessInfomation lpProcessInformation);

        [DllImport(dllName)]
        public static extern bool TerminateProcess(ProcessInfomation processInfomation, int exitCode);
    }
}