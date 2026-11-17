using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Payrollmanagementsystem.Models
{
    public class Employee
    { 
        public int Id { get; set; }
        public int  EmployeeCode { get; set; }
        public string EmployeeName { get; set; }
        public int DepartmentCode { get; set; }
        [ForeignKey(nameof(DepartmentCode))]
        public Department? Department { get; set; }
        public int DesignationCode { get; set; }
        [ForeignKey(nameof(DesignationCode))]
        public Designation? Designation { get; set; }
        public string EmployeeEmail { get; set; }
        public string EmployeePhone { get; set; }
        [Column(TypeName = "varchar(100)")]
        public DateTime? DateOfJoining { get; set; }
        public string Gender { get; set; }
        public string Address { get; set; }
        public bool IsActive { get; set; } = true;
        public DateTime? CreatedOn { get; set; } = DateTime.Now;
        public DateTime? UpdatedOn { get; set; }


    }
}
