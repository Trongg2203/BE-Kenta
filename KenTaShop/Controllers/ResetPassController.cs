using KenTaShop.Services;
using KenTaShop.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace KenTaShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResetPassController : ControllerBase
    {
        private IUserRepository _userRepo;

        public ResetPassController(IUserRepository userRepo)
        {
            _userRepo=userRepo;
        }
        [HttpPut("ResetPass")]
        public async Task<IActionResult> ResetPass(int    id)
        {
            return Ok(await _userRepo.ResetPass(id));
        }
        [HttpPut("ChangePass")]
        public async Task<IActionResult> ChangePass(ChangePass ChangePass)
        {
            return Ok(await _userRepo.ChangePass(ChangePass));
        }

    }
}
