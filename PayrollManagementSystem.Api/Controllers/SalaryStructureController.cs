using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PayrollManagementSystem.Services;
using PayrollManagementSystem.ViewModels;
using PayrollManagementSystem.ViewModels.Params;    
using PayrollManagementSystem.ViewModels.Result;

namespace PayrollManagementSystem.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class SalaryStructureController : ControllerBase
    {
        private readonly ISalaryStructure repo;
        public SalaryStructureController(ISalaryStructure _repo)
        {
            repo = _repo;


        }
        [HttpGet]
        public async Task<IActionResult> GetAllSalaryStructures(int PageNo, int PageSize)
        {
            return Ok(await repo.GetAllSalaryStructures(PageNo, PageSize));
        }
        [HttpPost]
        public async Task<IActionResult> AddSalaryStructure(SalaryStructureVm model)
        {
            return Ok(await repo.AddSalaryStructure(model));
        }
        [HttpPost]
        public async Task<IActionResult> UpdateSalaryStructure(SalaryStructureVm model)
        {
            return Ok(await repo.UpdateSalaryStructure(model));
        }
        [HttpPost]
        public async Task<IActionResult> DeleteSalaryStructure(int EmployeeCode)
        {
            return Ok(await repo.DeleteSalaryStructure(EmployeeCode));
        }
        [HttpGet]
        public async Task<IActionResult> GetSalaryStructurebyEmployeeCode(int employeeCode)
        {
            return Ok(await repo.GetSalaryStructureByEmployeeCode(employeeCode));
        }
        
       
        [HttpGet]
        public async Task<IActionResult> ActivateSalaryStructure(int EmployeeCode)
        {
            return Ok(await repo.ActivateSalaryStructure(EmployeeCode));
        }
        [HttpGet]
        public async Task<IActionResult> DeactivateSalaryStructure(int EmployeeCode)
        {
            return Ok(await repo.DeactivateSalaryStructure(EmployeeCode));
        }
        
        [HttpGet]
        public async Task<bool> IsSalaryStructureExists(int employeeCode)
        {
            return await repo.IsSalaryStructureExists(employeeCode);
        }
        
    }
}
