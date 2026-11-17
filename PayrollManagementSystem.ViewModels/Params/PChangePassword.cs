using System;
using System.Collections.Generic;
using System.Text;

namespace PayrollManagementSystem.ViewModels.Params
{
    public class PChangePassword
    {
        public string UserName { get; set; }
        public string OldPass { get; set; }
        public string NewPass { get; set; }
    }
}
