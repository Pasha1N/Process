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

        public Process(string processName, int processID)
        {
            this.processName = processName;
            this.processID = processID;
        }

        public ICollection<Process> Processes { get;}
        public int ProcessID => processID;
        public string ProcessName => processName;
    }
}