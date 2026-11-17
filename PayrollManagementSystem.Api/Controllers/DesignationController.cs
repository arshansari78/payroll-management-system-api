using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PayrollManagementSystem.Services;
using PayrollManagementSystem.ViewModels;
using PayrollManagementSystem.ViewModels.Result;
using Payrollmanagementsystem.Models;

namespace PayrollManagementSystem.Api.Controllers
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class DesignationController : ControllerBase
    {
        private readonly IDesignation repo;
        public DesignationController(IDesignation _repo)
        {
            repo = _repo;
        }
        [HttpPost]
        public async Task<IActionResult> AddDesignation(DesignationVm model)
        {
            return Ok(await repo.AddDesignation(model));
        }
        [HttpPut]
        public async Task<IActionResult> UpdateDesignation(DesignationVm model)
        {
            return Ok(await repo.UpdateDesignation(model));
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteDesignation(int DesignationCode)
        {
            return Ok(await repo.DeleteDesignation(DesignationCode));
        }
        
        [HttpGet]
        public async Task<IActionResult> GetDesignationByDesignationCode(int DesignationCode)
        {
            return Ok(await repo.GetDesignationByDesignationCode(DesignationCode));
        }

        [HttpGet]
        public async Task<IActionResult> GetAllDesignations(int PageNo, int PageSize)
        {
            return Ok(await repo.GetAllDesignations(PageNo, PageSize));
        }   
        
        [HttpGet]
        public async Task<IActionResult> SearchDesignation(string name)
        {
            return Ok(await repo.SearchDesignation(name));
        }
        
        [HttpGet]
        public async Task<IActionResult> ActivateDesignation(int DesignationCode)
        {
            return Ok(await repo.ActivateDesignation(DesignationCode));
        }
        [HttpGet]
        public async Task<IActionResult> DeactivateDesignation(int DesignationCode)
        {
            return Ok(await repo.DeactivateDesignation(DesignationCode));
        }

    }
}
