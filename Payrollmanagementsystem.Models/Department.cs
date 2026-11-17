using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Payrollmanagementsystem.Models;
using Microsoft.EntityFrameworkCore;


namespace Payrollmanagementsystem.Models
{
    public class Department
    {
        public int Id { get; set; }
        
       
        public string DeptName { get; set; }
        public int  DepartmentCode { get; set; }
        
        public string? Description { get; set; }
        public bool IsActive { get; set; }
        public DateTime? CreatedOn { get; set; }= DateTime.Now;
        public DateTime? UpdatedOn {  get; set; }
    }
}
