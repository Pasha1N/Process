using Process;
using Processes.Command;
using Processes.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Processes.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private int invalidValue = -1;
        private IList<ProcessViewModel> processViewModelsOrigin = new ObservableCollection<ProcessViewModel>();
        private IList<ProcessViewModel> processViewModels = new List<ProcessViewModel>();
        private ProcessViewModel selectProcessViewModel = null;
        private string commandLine = string.Empty;

        private ICommand commandForStopProcess;
        private ICommand commandForCreateProcess;
        private ICommand commandForRefresh;

        public MainViewModel()
        {
            commandForStopProcess = new DelegateCommand(StopProcess);
            commandForCreateProcess = new DelegateCommand(CreateProcess);
            commandForRefresh = new DelegateCommand(CollectionRefresh);
            GetProcessHandle();
            Sorting();
        }

        public ICommand CommandForCreateProcess => commandForCreateProcess;
        public ICommand CommandForStopProcess => commandForStopProcess;
        public ICommand CommandForRefresh => commandForRefresh;
        public IList<ProcessViewModel> Processes
        {
            get => processViewModelsOrigin;
            set
            {
                processViewModelsOrigin = value;
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(Processes)));
            }
        }
        public ProcessViewModel SelectProcessViewModel //не возвращает выделенный элемент
        {
            get => selectProcessViewModel;
            set
            {
                selectProcessViewModel = value;
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(SelectProcessViewModel)));
            }
        }
        public string CommandLine
        {
            get => commandLine;
            set
            {
                commandLine = value;
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(CommandLine)));
            }
        }


       public event PropertyChangedEventHandler PropertyChanged;

        public void GetProcessHandle()
        {
            int invalidValue = -1;
            IntPtr handle = MethodsFromUnmanagedCode.CreateToolhelp32Snapshot(0x00000002, 0);
            ProcessEntry processEntry = new ProcessEntry();
            processEntry.DwSize = Marshal.SizeOf<ProcessEntry>();

            if (handle != (IntPtr)invalidValue)
            {
                if (MethodsFromUnmanagedCode.FirstProcess(handle, ref processEntry))
                {
                    do
                    {
                        Models.Process process = new Models.Process(processEntry.ExeFile, processEntry.ProcessID, processEntry.ParentProcessID);
                        processViewModelsOrigin.Add(new ProcessViewModel( process));
                        processViewModels.Add(new ProcessViewModel(process));
                    }
                    while (MethodsFromUnmanagedCode.NextProcess(handle, ref processEntry));
                }
            }
        }

        public void Sorting()
        {
            for (int i = 1; i < processViewModels.Count; i++)
            {

                ProcessViewModel processModel = processViewModelSearch(processViewModelsOrigin, processViewModels[i].ParentProcessID);

                if (processModel != null)
                {
                    ProcessViewModel processViewModel = processViewModelSearch(processViewModelsOrigin, processViewModels[i].ProcessID);
                    processViewModelsOrigin.Remove(processViewModel);

                    processModel.ProcessesViewModel.Add(processViewModels[i]);
                }
            }
        }

        public ProcessViewModel processViewModelSearch(IEnumerable<ProcessViewModel> processModels, int processViewModelId)
        {
            ProcessViewModel processViewModel = null;

            foreach (ProcessViewModel process in processModels)
            {
                if (process.ProcessID == processViewModelId)
                {
                    processViewModel = process;
                    break;
                }

                if (process.ProcessesViewModel.Count > 0)
                {
                    processViewModel = processViewModelSearch(process.ProcessesViewModel, processViewModelId);                    
                }
                if (processViewModel != null)
                {
                    break;
                }
            }

            return processViewModel;
        }

        private void CreateProcess()
        {
            Startupinfoa startupinfoa = new Startupinfoa();
            startupinfoa.cb = Marshal.SizeOf<Startupinfoa>();
            ProcessInfomation processInfomation = new ProcessInfomation();            

            string apName = null;
            string currentDirectory = null;

            MethodsFromUnmanagedCode.CreateProcess(apName
                , commandLine
                , IntPtr.Zero
                , IntPtr.Zero
                , false
                , 0
                , IntPtr.Zero
                , currentDirectory
                , ref startupinfoa
                , ref processInfomation);

            if (Marshal.GetLastWin32Error() != 0)
            {
                Win32Exception errorMessage = new Win32Exception(Marshal.GetLastWin32Error());
                MessageBox.Show(errorMessage.Message, null, MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void StopProcess()
        {


        }

        public void CollectionRefresh()
        {
            processViewModelsOrigin.Clear();
            processViewModels.Clear();
            GetProcessHandle();
            Sorting();
            OnPropertyChanged(new PropertyChangedEventArgs(nameof(Processes)));
        }

        public void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            PropertyChanged?.Invoke(this, e);
        }

    }
}