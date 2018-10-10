using Process.ViewModels;
using System.Windows;

namespace Process.View
{
    public partial class MainWindow : Window
    {
        public MainWindow(ViewModelProcesses viewModelProcesses)
        {
            InitializeComponent();

            DataContext = viewModelProcesses;
        }
    }
}