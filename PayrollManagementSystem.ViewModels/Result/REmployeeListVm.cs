using System;
using System.Collections.Generic;
using System.Text;

namespace PayrollManagementSystem.ViewModels.Result
{
    public class REmployeeListVm
    {
        public ApiResultVm Result { get; set; }
        public List<EmployeeVm> Data { get; set; }
    }
}
