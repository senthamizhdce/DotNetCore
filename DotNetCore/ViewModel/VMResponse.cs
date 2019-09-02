using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetCore.ViewModel
{
    public class VMResponse 
    {
        public List<VMError> Error; 
        public Boolean Status;
        public string ErrorDetails;
        public string Source;
        public string DateTime; 
    }
}
