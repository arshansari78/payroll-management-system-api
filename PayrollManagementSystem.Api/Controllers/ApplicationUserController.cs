using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PayrollManagementSystem.Services;
using PayrollManagementSystem.ViewModels;
using PayrollManagementSystem.ViewModels.Params;

namespace PayrollManagementSystem.Api.Controllers
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class ApplicationUserController : ControllerBase
    {
        private readonly IApplicationUser repo;
        public ApplicationUserController(IApplicationUser _repo)
        {
            repo = _repo;
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Authenticate(PLoginVm model)

        {
            return Ok(await repo.Authenticate(model));
        }
        [HttpGet]
        [AllowAnonymous]
        
        public async Task<IActionResult> GetAll()
        {
            return Ok(await repo.GetAll());
        }
        [HttpPost]
        [AllowAnonymous]

        public async Task<IActionResult> ChangePassword(PChangePassword model)
        {
            return Ok(await repo.ChangePassword(model));
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Add(ApplicationUserVm model)
        {
            return Ok(await repo.Add(model));
        }
    }  
}
