using PayrollManagementSystem.ViewModels;
using PayrollManagementSystem.ViewModels.Result;
using System;
using System.Collections.Generic;
using System.Text;

namespace PayrollManagementSystem.Services
{
   
        

        public interface IDesignation
        {
            public Task<ApiResultVm> AddDesignation(DesignationVm model);

            public Task<ApiResultVm> UpdateDesignation(DesignationVm model);

            public Task<ApiResultVm> DeleteDesignation(int DesignationCode);
            public Task<RDesignationVm> GetDesignationByDesignationCode(int DesignationCode);

            public Task<RDesignationListVm> GetAllDesignations(int PageNo, int PageSize);

            public Task <RDesignationVm> SearchDesignation(string name);

            public Task<ApiResultVm> ActivateDesignation(int DesignationCode);
            public Task<ApiResultVm> DeactivateDesignation(int DesignationCode);

           
        }
    
}
