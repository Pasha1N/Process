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
        private ICollection<ProcessViewModel> processesViewModel = new List<ProcessViewModel>();

        public ProcessViewModel(Models.Process process)
        {
            this.process = process;
        }

        public int ProcessID => process.ProcessID;
        public string ProcessName => process.ProcessName;
        public ICollection<ProcessViewModel> ProcessesViewModel => processesViewModel;
        public int ParentProcessID => process.ParentProcessID;
    }
}