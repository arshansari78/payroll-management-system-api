using PayrollManagementSystem.ViewModels;
using PayrollManagementSystem.ViewModels.Result;
using System;
using System.Collections.Generic;
using System.Text;

namespace PayrollManagementSystem.Services
{
    public interface ISalaryStructure
    {
       
            Task<ApiResultVm> AddSalaryStructure(SalaryStructureVm model);

            Task<ApiResultVm> UpdateSalaryStructure(SalaryStructureVm model);

            Task<ApiResultVm> DeleteSalaryStructure(int EmployeeCode);

            Task<RSalaryStructureListVm> GetAllSalaryStructures(int pageNo, int pageSize);

            Task<RSalaryStructureVm> GetSalaryStructureByEmployeeCode(int EmployeeCode);

            Task<bool> IsSalaryStructureExists(int EmployeeCode);

            Task<ApiResultVm> ActivateSalaryStructure(int EmployeeCode);

            Task<ApiResultVm> DeactivateSalaryStructure(int EmployeeCode);
        
    }
}

