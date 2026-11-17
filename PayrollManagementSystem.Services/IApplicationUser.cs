using PayrollManagementSystem.ViewModels;
using PayrollManagementSystem.ViewModels.Params;
using PayrollManagementSystem.ViewModels.Result;
using System;
using System.Collections.Generic;
using System.Text;

namespace PayrollManagementSystem.Services
{
    public interface IApplicationUser
    {
        public Task<RLoginVm> Authenticate(PLoginVm model);

        public Task<RApplicationUserListVm> GetAll();

        public Task<ApiResultVm> ChangePassword(PChangePassword model);

        public Task<ApiResultVm> Add(ApplicationUserVm model);
    }
}
