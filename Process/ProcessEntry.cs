using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Process
{
    public struct ProcessEntry
    {
        public int DwSize { get; set; }
        public int CntUsage { get; set; }//0
        public int ProcessID { get; set; }//Идентификатор процесса.
        public int DefaultHeapID { get; set; }//0
        public int Th32ModuleID { get; set; }//0
        public int Thread { get; set; }//поток
        public int ParentProcessID { get; set; }//Идентификатор родительского процесса
        public long PcPriClassBase { get; set; }//Базовый приоритет любых потоков, созданных этим процессом. (что это?)
        public int Flags { get; set; }//0
        public string ExeFile { get; set; }//путь к exe
    }
}