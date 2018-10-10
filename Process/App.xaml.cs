using Process.ViewModels;
using System.Windows;

namespace Process
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            ViewModelProcesses viewModelProcesses = new ViewModelProcesses();
            Process.View.MainWindow mainWindow = new View.MainWindow(viewModelProcesses);
            mainWindow.Show();
        }
    }
}