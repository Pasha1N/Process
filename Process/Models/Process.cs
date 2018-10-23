using System.Collections.Generic;

namespace Processes.Models
{
    public class Process
    {
        private readonly int parentProcessID;
        private readonly int processID;
        private readonly string processName;

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