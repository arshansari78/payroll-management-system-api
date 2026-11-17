using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Text;

namespace PayrollManagementSystem.ViewModels.Result
{
    public class REmployeeVm
    {
        public ApiResultVm Result { get; set; }
        public EmployeeVm Data { get; set; }
    }
}
