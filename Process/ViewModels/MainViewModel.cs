using Processes.Command;
using Processes.Models;
using Processes.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Input;

namespace Processes.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private Processes.Command.Command commandForCreateProcess;
        private ICommand commandForRefresh;
        private string commandLine = string.Empty;
        private Processes.Command.Command commandForStopProcess;
        private bool enableCommandCreateProcess;
        private bool enableCommandStopProcess;
        private IList<ProcessViewModel> processViewModels = new List<ProcessViewModel>();
        private IList<ProcessViewModel> processViewModelsOrigin = new ObservableCollection<ProcessViewModel>();
        private ProcessViewModel selectedProcess = null;

        public MainViewModel()
        {
            commandForStopProcess = new DelegateCommand(StopProcess, EnableCommandStopProcess);
            commandForCreateProcess = new DelegateCommand(CreateProcess, EnableCommandCreateProcess);
            commandForRefresh = new DelegateCommand(CollectionRefresh);
            GetProcessHandle();
            Sorting();
        }

        public ICommand CommandForCreateProcess => commandForCreateProcess;

        public ICommand CommandForRefresh => commandForRefresh;

        public string CommandLine
        {
            get => commandLine;
            set
            {
                if (commandLine != value)
                {
                    commandLine = value;
                    OnPropertyChanged(new PropertyChangedEventArgs(nameof(CommandLine)));
                    enableCommandCreateProcess = commandLine.Length > 0;
                    commandForCreateProcess.OnCanExecuteChanged(EventArgs.Empty);
                }
            }
        }

        public ICommand CommandForStopProcess => commandForStopProcess;

        public IList<ProcessViewModel> Processes
        {
            get => processViewModelsOrigin;
        }

        public ProcessViewModel SelectedProcess
        {
            get => selectedProcess;
            set
            {
                if (selectedProcess != value)
                {
                    selectedProcess = value;
                    OnPropertyChanged(new PropertyChangedEventArgs(nameof(SelectedProcess)));
                    enableCommandStopProcess = selectedProcess != null;
                    commandForStopProcess.OnCanExecuteChanged(EventArgs.Empty);
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void CollectionRefresh()
        {
            processViewModelsOrigin.Clear();
            processViewModels.Clear();
            GetProcessHandle();
            Sorting();
            OnPropertyChanged(new PropertyChangedEventArgs(nameof(Processes)));
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

                ErrorMessageViewModel errorMessageViewModel = new ErrorMessageViewModel();
                ErrorMessage error = new ErrorMessage(errorMessageViewModel);
                errorMessageViewModel.Error = $"{errorMessage.Message}";
                error.ShowDialog();
            }
        }

        private bool EnableCommandCreateProcess()
        {
            return enableCommandCreateProcess;
        }

        private bool EnableCommandStopProcess()
        {
            return enableCommandStopProcess;
        }

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
                        processViewModelsOrigin.Add(new ProcessViewModel(process));
                        processViewModels.Add(new ProcessViewModel(process));
                    }
                    while (MethodsFromUnmanagedCode.NextProcess(handle, ref processEntry));
                }
            }
        }

        public void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            PropertyChanged?.Invoke(this, e);
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

        private void StopProcess()
        {
            IntPtr handleProcess = MethodsFromUnmanagedCode.OpenProcess(0x0001, false, SelectedProcess.ProcessID);
            MethodsFromUnmanagedCode.TerminateProcess(handleProcess, 0);
            Win32Exception errorMessage = new Win32Exception(Marshal.GetLastWin32Error());

            if (Marshal.GetLastWin32Error() != 0)
            {
                ErrorMessageViewModel errorMessageViewModel = new ErrorMessageViewModel();
                ErrorMessage error = new ErrorMessage(errorMessageViewModel);
                errorMessageViewModel.Error = $"{errorMessage.Message}";
                error.ShowDialog();
            }
        }
    }
}