using Processes.View;
using Processes.ViewModels;

namespace Processes.ErrorDisplay
{
    public class ShowErrorMessage : IShowErrorMessage
    {
        public void ShowMessage(string message)
        {
            ErrorMessageViewModel errorMessageViewModel = new ErrorMessageViewModel();
            ErrorMessage error = new ErrorMessage(errorMessageViewModel);
            errorMessageViewModel.Error = $"{message}";
            error.ShowDialog();
        }
    }
}