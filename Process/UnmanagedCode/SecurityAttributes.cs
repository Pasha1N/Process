using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Processes.Models
{
    public struct SecurityAttributes
    {
        public int nLength;
        public int lpSecurityDescriptor;
        public bool bInheritHandle;
    }
}