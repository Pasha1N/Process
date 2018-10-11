using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Process.ViewModels
{
    public class ViewModelProcesses
    {
        IntPtr intPtr = MethodsFromUnmanagedCode.CreateToolhelp32Snapshot(0x00000002, 0);

        int a = 0;
    }
}