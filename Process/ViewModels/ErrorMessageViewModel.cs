using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Processes.ViewModels
{
    public class ErrorMessageViewModel
    {
        private string error = "sdfsd";

        public string Error
        {
            get => error;
            set => error = value;
        }
    }
}