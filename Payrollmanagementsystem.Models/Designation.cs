using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Payrollmanagementsystem.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Microsoft.EntityFrameworkCore;
 

namespace Payrollmanagementsystem.Models
{
    public class Designation
    {
        public int Id { get; set; }
        
        public string DesignationName { get; set; }
        public int DesignationCode { get; set; }
        
        public string? Description { get; set; }
        public bool IsActive { get; set; } = true;
        public DateTime? createdOn { get; set; }=DateTime.Now;
        public DateTime? updatedOn { get; set; }
    }
}
