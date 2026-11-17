using PayrollManagementSystem.ViewModels;
using PayrollManagementSystem.ViewModels.Result;
using System;
using System.Collections.Generic;
using System.Text;

namespace PayrollManagementSystem.Services
{
    
        public interface IPayroll
        {
           public Task<ApiResultVm> GeneratePayroll(PayrollVm model);

            public Task<ApiResultVm> UpdatePayroll(PayrollVm model);

            public Task<ApiResultVm> DeletePayroll(int EmployeeCode);
            public Task<RPayrollVm> GetPayrollByEmployeeCode(int EmployeeCode);

            public Task<RPayrollListVm> GetAllPayrolls(int PageNo, int PageSize);

            public Task<RPayrollVm> GetPayrollByEmployeeName(string employeeName);

            public Task<RPayrollVm> GetPayrollByMonth(int month, int year);
            public Task<RPayrollVm> GetPayrollByDepartment(int DepartmentCode);

            public Task<RPayrollVm> GetPayrollByDesignation(int DesignationCode);

            public Task<bool> IsPayrollGenerated(int employeeId, int month, int year);
        }
    
}
