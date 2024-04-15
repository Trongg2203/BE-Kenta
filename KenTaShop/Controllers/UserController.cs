using KenTaShop.Services;
using KenTaShop.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KenTaShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepo;

        public UserController(IUserRepository userRepo)
        {
            _userRepo = userRepo;
        }
        [HttpGet("All")]     
        public async Task<IActionResult> GetAll()
        {
            var users = await _userRepo.GetAll();
            return Ok(users);
        }

        [HttpPost("IdUser")]
        public async Task<IActionResult> AddUser([FromForm] InforUser inforuser)
        {
            var users = await _userRepo.AddUser(inforuser);
            return Ok(users);
        }

        [HttpPut("EditByIdUser")]
        public async Task<IActionResult> EditByIdUser([FromForm] int idUser, [FromForm] InforUser inforuser)
        {
            var user = await _userRepo.EditUser(idUser, inforuser);
            return Ok(user);
        }

        [HttpDelete("DeleteById")]
        public async Task<IActionResult> DeleteById([FromForm] int idUser)
        {
            var user = await _userRepo.DeleteById(idUser);
            return Ok(user);
        }
    }
}
