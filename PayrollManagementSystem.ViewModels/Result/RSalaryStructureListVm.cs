using System;
using System.Collections.Generic;
using System.Text;

namespace PayrollManagementSystem.ViewModels.Result
{
    public class RSalaryStructureListVm
    {
        public ApiResultVm Result { get; set; }
        public List<SalaryStructureVm> Data { get; set; }
    }
}
