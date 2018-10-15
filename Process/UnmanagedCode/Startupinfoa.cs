using System;

namespace Processes.Models
{
    public struct Startupinfoa
    {
        public int cb;//Structure size
        public string lpReserved;
        public string lpDesktop;//Name desktop
        public string lpTitle;//for console
        public int dwX;
        public int dwY;
        public int dwXSize;
        public int dwYSize;
        public int dwXCountChars;
        public int dwYCountChars;
        public int dwFillAttribute;
        public int dwFlags;
        public short wShowWindow;
        public short cbReserved2;
        public byte lpReserved2;
        public IntPtr hStdInput;
        public IntPtr hStdOutput;
        public IntPtr hStdError;
    }
}