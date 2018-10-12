using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Processes.ViewModels
{
    public class ProcessViewModel
    {
        private Models.Process process;

        public ProcessViewModel(Models.Process process)
        {
            this.process = process;
        }

        public int ProcessID => process.ProcessID;
        public string ProcessName => process.ProcessName;
        public ICollection<ProcessViewModel> ProcessesViewModel { get; }
    }
}