using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace PayrollManagementSystem.ViewModels
{
    public class PayrollVm
    {
        public int Id { get; set; }
        public int EmployeeCode { get; set; }
       
        public string EmployeeName { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        public decimal BasicPay { get; set; }
        public decimal Allowances { get; set; }
        public decimal Deductions { get; set; }

        public decimal NetPay { get; set; }
        public DateTime GeneratedDate { get; set; } = DateTime.Now;
    }
}
