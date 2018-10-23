using Processes.ViewModels;
using System.Windows;

namespace Processes.View
{
    public partial class ErrorMessage : Window
    {
        public ErrorMessage(ErrorMessageViewModel errorMessageViewModel)
        {
            InitializeComponent();

            DataContext = errorMessageViewModel;
        }
    }
}