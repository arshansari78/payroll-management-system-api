using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PayrollManagementSystem.ViewModels
{
    public class DesignationVm
    {
        public int Id { get; set; }
        
        public string DesignationName { get; set; }
        public int DesignationCode { get; set; }
        public string? Description { get; set; }
        public bool IsActive { get; set; } = true;
        public DateTime? createdOn { get; set; } = DateTime.Now;
        public DateTime? updatedOn { get; set; }
    }
}
