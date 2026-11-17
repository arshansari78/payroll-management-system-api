using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PayrollManagementSystem.Services;
using PayrollManagementSystem.ViewModels;
using PayrollManagementSystem.ViewModels.Params;
using PayrollManagementSystem.ViewModels.Result;
using Payrollmanagementsystem.Models;



namespace PayrollManagementSystem.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PayrollController : ControllerBase
    {
        private readonly IPayroll repo;
        public PayrollController(IPayroll _repo)
        {
            repo = _repo;


        }
        [HttpPost]
        public async Task<IActionResult> GeneratePayroll(PayrollVm model)
        {
            return Ok(await repo.GeneratePayroll(model));
        }
        [HttpGet]
        public async Task<IActionResult> GetPayroll(int employeeCode)
        {
            return Ok(await repo.GetPayrollByEmployeeCode(employeeCode));
        }
        [HttpGet]
        public async Task<IActionResult> GetAllPayrolls(int PageNo, int PageSize)
        {
            return Ok(await repo.GetAllPayrolls(PageNo, PageSize));
        }
        [HttpGet]
        public async Task<IActionResult> GetPayrollByEmployeeName(string employeeName)
        {
            return Ok(await repo.GetPayrollByEmployeeName(employeeName));
        }
        [HttpGet]
        public async Task<IActionResult> GetPayrollByMonth(int month, int year)
        {
            return Ok(await repo.GetPayrollByMonth(month, year));
        }
        [HttpGet]
        public async Task<IActionResult> GetPayrollByDepartment(int DepartmentCode)
        {
            return Ok(await repo.GetPayrollByDepartment(DepartmentCode));
        }
        [HttpGet]
        public async Task<IActionResult> GetPayrollByDesignation(int DesignationCode)
        {
            return Ok(await repo.GetPayrollByDesignation(DesignationCode));
        }
        
        
        [HttpGet]
        public async Task<IActionResult> IsPayrollGenerated(int employeeCode, int month, int year)
        {
            return Ok(await repo.IsPayrollGenerated(employeeCode, month, year));
        }
        [HttpPut]
        public async Task<IActionResult> UpdatePayroll(PayrollVm model)
        {
            return Ok(await repo.UpdatePayroll(model));
        }
        [HttpDelete]
        public async Task<IActionResult> DeletePayroll(int employeeCode)
        {
            return Ok(await repo.DeletePayroll(employeeCode));
        }
        

    }
}
