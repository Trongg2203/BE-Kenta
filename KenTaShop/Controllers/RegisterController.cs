using KenTaShop.Services;
using KenTaShop.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KenTaShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterController : ControllerBase
    {
        private readonly IUserRepository _userRepo;

        public RegisterController(IUserRepository userRepo)
        {
            _userRepo = userRepo;
        }
        [HttpPost("Register")]
        public async Task<IActionResult>  Register(Register register)
        {
            return Ok(await _userRepo.Register(register));
        }
    }
}
