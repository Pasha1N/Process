using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Processes.Models
{
    public class Process
    {
        private string processName;
        private int processID;
        private int parentProcessID;

        public Process(string processName, int processID, int parentProcessID)
        {
            this.processName = processName;
            this.processID = processID;
            this.parentProcessID = parentProcessID;
        }

        public ICollection<Process> Processes { get;}
        public int ProcessID => processID;
        public string ProcessName => processName;
        public int ParentProcessID => parentProcessID;
    }
}