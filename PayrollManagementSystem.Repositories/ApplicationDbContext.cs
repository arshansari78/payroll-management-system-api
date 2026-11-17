using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Payrollmanagementsystem.Models;
using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Text;


namespace PayrollManagementSystem.Repositories
{
    public class ApplicationDbContext:IdentityDbContext
    {
        
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<ApplicationUser> Users { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Designation> Designations { get; set; }
        public DbSet<Payroll> Payrolls { get; set; }
        public DbSet<SalaryStructure> SalaryStructures { get; set; }
        

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            var hashed = new PasswordHasher<ApplicationUser>();
            builder.Entity<ApplicationUser>().HasData(new ApplicationUser
            {
                Id = "administrator",
                UserName = "administrator",
                NormalizedUserName = "administrator",
                FullName = "Power User",
                Email = "admin@example.com",
                PhoneNumber = "1234567890",
                PasswordHash = hashed.HashPassword(null, "Admin@2020")


            });

            builder.Entity<Department>().HasAlternateKey(d => d.DepartmentCode);
            builder.Entity<Designation>().HasAlternateKey(d => d.DesignationCode);

            builder.Entity<Employee>()
                .HasOne(e => e.Department).WithMany()
                .HasForeignKey(e => e.DepartmentCode)
                .HasPrincipalKey(d => d.DepartmentCode);

            builder.Entity<Employee>()
                .HasOne(e => e.Designation).WithMany()
                .HasForeignKey(e => e.DesignationCode)
                .HasPrincipalKey(d => d.DesignationCode);

            builder.Entity<SalaryStructure>()
     .HasOne(s => s.Employee)
     .WithMany()
     .HasForeignKey(s => s.EmployeeCode)
     .HasPrincipalKey(e => e.EmployeeCode)
     .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Payroll>()
    .HasOne(p => p.Employee)
    .WithMany()
    .HasForeignKey(p => p.EmployeeCode)
    .HasPrincipalKey(e => e.EmployeeCode)
    .OnDelete(DeleteBehavior.Cascade);


        }
    }
}

