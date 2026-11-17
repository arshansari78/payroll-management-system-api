using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using PayrollManagementSystem.ViewModels.Result;
using PayrollManagementSystem.ViewModels;


namespace PayrollManagementSystem.ViewModels
{
    public class EmployeeVm
    {
        public int Id { get; set; }
        public int EmployeeCode { get; set; }
        public string EmployeeName { get; set; }
        public string EmployeeEmail { get; set; }
        public string EmployeePhone { get; set; }
        [Column(TypeName = "varchar(100)")]

        public int DepartmentCode { get; set; }
        public int DesignationCode { get; set; }
        public DateTime? DateOfJoining { get; set; }
        public string Gender { get; set; }
        public string Address { get; set; }
        
        
        public bool IsActive { get; set; } = true;
        public DateTime? CreatedOn { get; set; } = DateTime.Now;
        public DateTime? UpdatedOn { get; set; }
    }
}
