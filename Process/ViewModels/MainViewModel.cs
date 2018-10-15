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
        private ICollection<ProcessViewModel> processViewModels = new List<ProcessViewModel>();
        private IList<Models.Process> processes = new List<Models.Process>();
        private ICommand commandForStopProcess;
        private ICommand commandForCreateProcess;
        private ICommand commandForRefresh;

        public MainViewModel()
        {
            commandForStopProcess = new DelegateCommand(StopProcess);
            commandForCreateProcess = new DelegateCommand(CreateProcess);
            GetProcessHandle();
            Sorting();
            CreateProcess();
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
                        processes.Add(process);
                    }
                    while (MethodsFromUnmanagedCode.NextProcess(handle, ref processEntry));
                }
            }
        }

        public void Sorting()
        {
            bool work = true;

          //  while (work)
          //  {
                for (int j = 0; j < processes.Count; j++)
                {
                if (processes[j].ParentProcessID != 0)
                {
                    ProcessViewModel processViewModel = null;
                    processViewModel = processViewModelSearch(processViewModels ,processes[j].ParentProcessID);

                    if (processViewModel != null)
                    {
                        ProcessViewModel processModel = new ProcessViewModel(processes[j]);
                        processes.Remove(processes[j]);
                        processModel.ProcessesViewModel.Add(processViewModel);
                    }
                    else
                    {
                        ProcessViewModel processModel = new ProcessViewModel(processes[j]);
                        Models.Process parentProcess = ProcessSearch(processes[j].ParentProcessID);
                        ProcessViewModel parentProcessModel = new ProcessViewModel(parentProcess);
                        parentProcessModel.ProcessesViewModel.Add(processModel);
                        processes.Remove(processes[j]);
                        processes.Remove(parentProcess);
                        processViewModels.Add(parentProcessModel);
                    }
                }
                }
           // }
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
                    processViewModelSearch(process.ProcessesViewModel, processViewModelId);
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
            string commandLine = "notepad";
            string apName = null;
            string currentDirectory = null;
            int? lpEnvironment = null;

            MessageBox.Show("hi my Freind");

            //MethodsFromUnmanagedCode.CreateProcess(ref apName
            //    , ref commandLine
            //    , null
            //    , null
            //    , false
            //    , 0
            //    , ref lpEnvironment
            //    , ref currentDirectory
            //    , ref startupinfoa
            //    , ref processInfomation);
        }

        private void StopProcess()
        {

        }

    }
}