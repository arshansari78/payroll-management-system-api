using System;
using System.Collections.Generic;
using System.Text;

namespace PayrollManagementSystem.ViewModels.Result
{
    public class RDepartmentListVm
    {
        public ApiResultVm Result { get; set; }
        public List<DepartmentVm> Data { get; set; }
    }
}
