using Processes.ViewModels;
using System.Windows;

namespace Process
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            MainViewModel mainViewModel = new MainViewModel();
            Process.View.MainWindow mainWindow = new View.MainWindow(mainViewModel);
            mainWindow.Show();
        }
    }
}