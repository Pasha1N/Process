using System;

namespace Processes.Models
{
    public struct ProcessInfomation
    {
        private IntPtr hProcess;
        private IntPtr hThread;
        private int dwProcessId;
        private int dwThreadId;
    }
}