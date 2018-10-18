using Process;
using Processes.Command;
using Processes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Processes.ViewModels
{
    public class MainViewModel
    {
        private const string dllName = @"Kernel32.dll";
        private int invalidValue = -1;
        private IList<ProcessViewModel> processViewModels = new List<ProcessViewModel>();
        private IList<ProcessViewModel> processViewModels1 = new List<ProcessViewModel>();

        private IList<Models.Process> processes = new List<Models.Process>();//IColaction
        private ICommand commandForStopProcess;
        private ICommand commandForCreateProcess;
        private ICommand commandForRefresh;

        public MainViewModel()
        {
            commandForStopProcess = new DelegateCommand(StopProcess);
            commandForCreateProcess = new DelegateCommand(CreateProcess);
            GetProcessHandle();
            Sorting1();
        }

        public ICommand CommandForCreateProcess => commandForCreateProcess;
        public ICommand CommandForStopProcess => commandForStopProcess;
        public ICommand CommandForRefresh => commandForRefresh;
        public IEnumerable<ProcessViewModel> Processes => processViewModels;

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
                        //processes.Add(process);
                        processViewModels.Add(new ProcessViewModel( process));
                        processViewModels1.Add(new ProcessViewModel(process));
                    }
                    while (MethodsFromUnmanagedCode.NextProcess(handle, ref processEntry));
                }
            }
        }

        public void Sorting()
        {
            int a = 0;
            for (int j = 1; j < processes.Count; j++)
            {
                ProcessViewModel processViewModel = null;
                processViewModel = processViewModelSearch(processViewModels, processes[j].ParentProcessID);

                if (processViewModel != null)
                {
                    ProcessViewModel processModel = new ProcessViewModel(processes[j]);
                    processes.Remove(processes[j]);
                    processViewModel.ProcessesViewModel.Add(processModel);
                }
                else
                {
                    ProcessViewModel processModel = new ProcessViewModel(processes[j]);
                    Models.Process parentProcess = ProcessSearch(processModel.ParentProcessID);

                    if (parentProcess != null)
                    {
                        ProcessViewModel parentProcessModel = new ProcessViewModel(parentProcess);
                        parentProcessModel.ProcessesViewModel.Add(processModel);
                        //processes.Remove(processes[j]);
                       // processes.Remove(parentProcess);
                        processViewModels.Add(parentProcessModel);
                    }
                    else
                    {
                        ProcessViewModel processModel1 = new ProcessViewModel(processes[j]);
                        processViewModels.Add(processModel1);
                    }
                }
                a = j;
            }
            int d = a;
        }

        public void Sorting1()
        {
            for (int i = 1; i < processViewModels1.Count; i++)
            {

                ProcessViewModel processModel = processViewModelSearch(processViewModels, processViewModels1[i].ParentProcessID);

                if (processModel != null)
                {
                    ProcessViewModel processViewModel = processViewModelSearch(processViewModels, processViewModels1[i].ProcessID);
                    processViewModels.Remove(processViewModel);

                    processModel.ProcessesViewModel.Add(processViewModels1[i]);
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

        public Models.Process ProcessSearch(int processId)
        {
            Models.Process desiredProcess = null;

            foreach (Models.Process process in processes)
            {
                if (process.ProcessID == processId)
                {
                    desiredProcess = process;
                    break;
                }
            }
            return desiredProcess;
        }


        private void CreateProcess()
        {
            Startupinfoa startupinfoa = new Startupinfoa();
            startupinfoa.cb = Marshal.SizeOf<Startupinfoa>();
            ProcessInfomation processInfomation = new ProcessInfomation();
            //string commandLine = "calc";
            string commandLine = "cadcflc";//как узнать зупустился ли процесс?

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
        }

        private void StopProcess()
        {

        }

    }
}