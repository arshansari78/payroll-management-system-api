using PayrollManagementSystem.ViewModels;
using PayrollManagementSystem.ViewModels.Result;
using System;
using System.Collections.Generic;
using System.Text;

namespace PayrollManagementSystem.Services
{
    public interface IDepartment
    {
        public Task<ApiResultVm> Add(DepartmentVm model);
        public Task<ApiResultVm> UpdateDepartment(DepartmentVm model);
        public Task<ApiResultVm> DeleteDepartment( int DepartmentCode);
        public Task<RDepartmentVm> GetDepartmentByDepartmentCode(int DepartmentCode);
        public Task<RDepartmentListVm> GetAllDepartments(int PageNo,int PageSize);
        public Task<RDepartmentVm> SearchDepartment(int DepartmentCode);
        public Task<ApiResultVm> ActivateDepartment(int DepartmentCode);
        public Task<ApiResultVm> DeactivateDepartment(int DepartmentCode);
        public Task<bool> IsDepartmentNameExists(string Deptname);
        public Task<bool> IsDepartmentCodeExists(string code);
        public Task<int> GetEmployeeCountByDepartment(int DepartmentCode);

    }
}
