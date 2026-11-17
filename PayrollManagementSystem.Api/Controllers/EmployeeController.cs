using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PayrollManagementSystem.Services;
using System.Security.Cryptography.X509Certificates;
using PayrollManagementSystem.ViewModels;

namespace PayrollManagementSystem.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployee repo;
        public EmployeeController(IEmployee _repo)
        {
            repo = _repo;


        }
        [HttpGet]
        public async Task<IActionResult> GetAllEmployees(int PageNo, int PageSize)
        {
            return Ok(await repo.GetAllEmployees(PageNo, PageSize));
        }
        [HttpGet]
        public async Task<IActionResult> GetEmployeeByEmployeeCode(int EmployeeCode)
        {
            return Ok(await repo.GetEmployeeByEmployeeCode(EmployeeCode));
        }
        [HttpGet]
        public async Task<IActionResult> GetEmployeeByName(string name)
        {
            return Ok(await repo.GetEmployeeByName(name));
        }
        [HttpGet]
        public async Task<IActionResult> GetEmployeeByDepartment(int departmentCode)
        {
            return Ok(await repo.GetEmployeeByDepartment(departmentCode));
        }
        [HttpGet]
        public async Task<IActionResult> GetEmployeeByDesignation(int designationCode)
        {
            return Ok(await repo.GetEmployeeByDesignation(designationCode));
        }
        [HttpPost]
        public async Task<IActionResult> Add(EmployeeVm model)
        {
            return Ok(await repo.Add(model));
        }
        [HttpPut]
        public async Task<IActionResult> UpdateEmployee(EmployeeVm model)
        {
            return Ok(await repo.UpdateEmployee(model));
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteEmployee(int EmployeeCode)
        {
            return Ok(await repo.DeleteEmployee(EmployeeCode));
        }
        [HttpPut]
        public async Task<IActionResult> ActivateEmployee(int EmployeeCode)
        {
            return Ok(await repo.ActivateEmployee(EmployeeCode));
        }
        [HttpPut]
        public async Task<IActionResult> DeactivateEmployee(int EmployeeCode)
        {
            return Ok(await repo.DeactivateEmployee(EmployeeCode));
        }
        [HttpGet]   
        public async Task<bool> IsEmailExists(string email)
        {
            return await repo.IsEmailExists(email);
        }
        [HttpGet]
        public async Task<bool> IsPhoneNumberExists(string phoneNumber)
        {
            return await repo.IsPhoneNumberExists(phoneNumber);
        }


    }
}
