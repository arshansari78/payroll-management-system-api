using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PayrollManagementSystem.Services;
using PayrollManagementSystem.ViewModels;

namespace PayrollManagementSystem.Api.Controllers
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartment repo;
        public DepartmentController(IDepartment _repo)
        {
            repo = _repo;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllDepartments(int PageNo, int PageSize)
        {
            return Ok(await repo.GetAllDepartments(PageNo, PageSize));
        }
        [HttpPost]
        public async Task<IActionResult> Add(DepartmentVm model)
        {
            return Ok(await repo.Add(model));
        }
        [HttpPost]
        public async Task<IActionResult> UpdateDepartment(DepartmentVm model)
        {
            return Ok(await repo.UpdateDepartment(model));

        }
        [HttpPost]
        public async Task<IActionResult> DeleteDepartment(int DepartmentCode)
        {
            return Ok(await repo.DeleteDepartment(DepartmentCode));
        }
        [HttpGet]
        public async Task<IActionResult> GetDepartmentByDepartmentCode(int DepartmentCode)
        {
            return Ok(await repo.GetDepartmentByDepartmentCode(DepartmentCode));
        }
        [HttpGet]
        public async Task<IActionResult> GetEmployeeCountByDepartment(int DepartmentCode)
        {
            return Ok(await repo.GetEmployeeCountByDepartment(DepartmentCode));
        }
        [HttpGet]
        public async Task<bool> IsDepartmentNameExists(string Deptname)
        {
            return await repo.IsDepartmentNameExists(Deptname);
        }
        [HttpGet]
        public async Task<bool> IsDepartmentCodeExists(int DepartmentCode) {
            return await repo.IsDepartmentCodeExists(DepartmentCode.ToString());
        }
        [HttpGet]
        public async Task<IActionResult> SearchDepartment(int DepartmentCode)
        {
            return Ok(await repo.SearchDepartment(DepartmentCode));
        }
        [HttpGet]
        public async Task<IActionResult> ActivateDepartment(int DepartmentCode)
        {
            return Ok(await repo.ActivateDepartment(DepartmentCode));
        }
        [HttpGet]
        public async Task<IActionResult> DeactivateDepartment(int DepartmentCode)
        {
            return Ok(await repo.DeactivateDepartment(DepartmentCode));
        } 

    }
}
