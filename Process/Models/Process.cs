using System.Collections.Generic;

namespace Processes.Models
{
    public class Process
    {
        private int parentProcessID;
        private int processID;
        private string processName;

        public Process(string processName, int processID, int parentProcessID)
        {
            this.processName = processName;
            this.processID = processID;
            this.parentProcessID = parentProcessID;
        }

        public int ParentProcessID => parentProcessID;
        public ICollection<Process> Processes { get; }
        public int ProcessID => processID;
        public string ProcessName => processName;
    }
}