using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Payrollmanagementsystem.Models
{
    public class SalaryStructure
    {
        public int Id { get; set; }
        public int EmployeeCode { get; set; }
        [ForeignKey(nameof(EmployeeCode))]
        public Employee? Employee { get; set; }
        public decimal BasicPay { get; set; }
        public decimal Allowances { get; set; }
         public decimal Deductions { get; set; }
        public DateTime? CreatedOn { get; set; }= DateTime.Now;
        public DateTime? LastUpdatedOn { get; set; }
        public DateTime? EffectiveFrom { get; set; }
        public DateTime? EffectiveTo { get; set; }
        public bool IsActive { get; set; }

    }
}
