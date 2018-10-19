using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Processes.ViewModels
{
    public class ProcessViewModel
    {
        private Models.Process process;
        private ICollection<ProcessViewModel> processesViewModel = new ObservableCollection<ProcessViewModel>();

        public ProcessViewModel(Models.Process process)
        {
            this.process = process;
        }

        public int ParentProcessID => process.ParentProcessID;
        public int ProcessID => process.ProcessID;
        public ICollection<ProcessViewModel> ProcessesViewModel => processesViewModel;
        public string ProcessName => process.ProcessName;
    }
}