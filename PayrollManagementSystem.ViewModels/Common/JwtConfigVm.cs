using System;
using System.Collections.Generic;
using System.Text;

namespace PayrollManagementSystem.ViewModels.Common
{
    public class JwtConfigVm
    {
        public string Key { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public int ExpiresInMinutes { get; set; }
        public string Subject { get; set; }
    }
}
