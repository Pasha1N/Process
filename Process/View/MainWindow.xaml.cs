using Processes.ViewModels;
using System.Windows;

namespace Process.View
{
    public partial class MainWindow : Window
    {
        public MainWindow(MainViewModel mainViewModel)
        {
            InitializeComponent();

            DataContext = mainViewModel;
        }
    }
}