using System.Runtime.InteropServices;

namespace Processes
{
    public struct ProcessEntry
    {
        public int DwSize;
        public int CntUsage;//0
        public int ProcessID;//Идентификатор процесса. 
        public int DefaultHeapID;//0
        public int Th32ModuleID;//0
        public int Thread;//поток
        public int ParentProcessID;//Идентификатор родительского процесса
        public int PcPriClassBase;//Базовый приоритет любых потоков, созданных этим процессом. (что это?)
        public int Flags;//0
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
        public string ExeFile;//путь к exe
    }
}