using KenTaShop.Services;
using KenTaShop.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KenTaShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserTypeController : ControllerBase
    {
        private readonly IUserTypeRepositoty _usertypeRepo;

        public UserTypeController(IUserTypeRepositoty usertypeRepo)
        {
            _usertypeRepo = usertypeRepo;
        }

        [HttpGet("GetAll")]
        public async Task< IActionResult> GetAll()
        {
            var loais = await _usertypeRepo.GetAll();
            return Ok(loais);
        }

        [HttpGet("GetById")]
        public async Task<IActionResult> GetById(int idUserType)
        {
            var loais = await _usertypeRepo.GetById(idUserType);
            return Ok(loais);
        }

        [HttpPost("AddUserType")]
        public async Task<IActionResult> AddUserType([FromForm] UserTypeVM usertypevm)
        {
            var loais = await _usertypeRepo.AddUserType(usertypevm);
            return Ok(loais);
        }

        [HttpPut("EditUserTypeById")]
        public async Task<IActionResult> EditUserTypeById([FromForm] int idUsertype, [FromForm] EditUserTypeMD edit)
        {
            var loais = await _usertypeRepo.EditUserTypeById(idUsertype, edit);
            return Ok(loais);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteUserType([FromForm] int idUserType)
        {
            var loais = await _usertypeRepo.DeleteUserType(idUserType);
            return Ok(loais);
        }

    }
}
