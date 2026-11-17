using PayrollManagementSystem.ViewModels;
using PayrollManagementSystem.ViewModels.Result;
using System;
using System.Collections.Generic;
using System.Text;

namespace PayrollManagementSystem.Services
{
    public interface IEmployee
    {
        public Task<ApiResultVm> Add(EmployeeVm model);
        public Task<ApiResultVm> UpdateEmployee(EmployeeVm model);
        public Task<ApiResultVm> DeleteEmployee(int EmployeeCode);
        public Task<REmployeeVm> GetEmployeeByEmployeeCode(int EmployeeCode);
        public Task<REmployeeListVm> GetAllEmployees(int PageNo, int PageSize);
        public Task<REmployeeVm> GetEmployeeByName(string name);
        public Task<REmployeeVm> GetEmployeeByDepartment(int DepartmentCode);
        public Task<REmployeeVm> GetEmployeeByDesignation(int DesignationCode);
        public Task<ApiResultVm> ActivateEmployee(int EmployeeCode);
        public Task<ApiResultVm> DeactivateEmployee(int EmployeeCode);
        public Task<bool> IsEmailExists(string email);
        public Task<bool> IsPhoneNumberExists(string phoneNumber);

    }
}
