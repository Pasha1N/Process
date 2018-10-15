using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Processes.Models
{
    public struct ProcessInfomation
    {
        IntPtr hProcess;
        IntPtr hThread;
        int dwProcessId;
        int dwThreadId;
    }
}