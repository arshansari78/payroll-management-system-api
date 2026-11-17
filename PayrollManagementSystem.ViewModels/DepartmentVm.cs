using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PayrollManagementSystem.ViewModels
{
    public class DepartmentVm
    {
        public int Id { get; set; }
        
       
        public string DeptName { get; set; }
        public int DepartmentCode { get; set; }
        public string? Description { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.Now;
        public DateTime? UpdatedOn { get; set; }
    }
}
