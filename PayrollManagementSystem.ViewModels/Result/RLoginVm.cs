using System;
using System.Collections.Generic;
using System.Text;

namespace PayrollManagementSystem.ViewModels.Result
{
    public class RLoginVm
    {
        public string UserName { get; set; }
        public string Token { get; set; }
        public ApiResultVm Result { get; set; }
    }
}
