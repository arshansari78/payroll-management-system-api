using System;
using System.Collections.Generic;
using System.Text;

namespace PayrollManagementSystem.ViewModels.Result
{
    public class RPayrollListVm
    {
        public ApiResultVm Result { get; set; }
        public List<PayrollVm> Data { get; set; }
    }
}
