using KenTaShop.Data;
using KenTaShop.Services;
using KenTaShop.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace KenTaShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepo;
        private readonly IConfiguration _configuration;
        private readonly ClothesShopManagementContext _dbcontext;
        private readonly PasswordHasher passwordHash;

        public UserController(IUserRepository userRepo, IConfiguration configuration, ClothesShopManagementContext dbcontext)
        {
            _userRepo = userRepo;
            _configuration = configuration;
            _dbcontext = dbcontext;
            passwordHash = new PasswordHasher();
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

        [HttpPost("AdminAdd")]
        public async Task<IActionResult> AdminAdd([FromForm] AdminAdd adminadd)
        {
            var adadd = await _userRepo.AdminAdd(adminadd);
            return Ok(adadd);
        }
        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult<AuthResponse>> Login(Login login)
        {
            //=(37U=
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var user = await (from l in _dbcontext.Users where l.Email == login.Email select l).SingleOrDefaultAsync();
            if (user is null)
            {
                return Unauthorized(new AuthResponse()
                {
                    IsSuccess = false,
                    Message = "User not found with this email"
                });
            }

            var result = passwordHash.verifyPassword(login.Password!, user.Pass!);
            if (result)
            {
                return Unauthorized(new AuthResponse
                {
                    IsSuccess = false,
                    Message = "mat khau khong dung"
                });
            }
            var token = GenerateToken(user);
            return Ok(new AuthResponse
            {
                Token = token,
                IsSuccess = true,
                Message = "LoginSuccess"
            });
        }
        [NonAction]
        private string GenerateToken(User user)
        {
            var tokenHandel = new JwtSecurityTokenHandler();

            var key = Encoding.UTF8.GetBytes(_configuration.GetSection("JWT").GetSection("Access_Secret").Value!);
            var role = (from r in _dbcontext.Userstypes where r.IdUsertype == user.IdUsertype select r).SingleOrDefault();
            List<Claim> claims = [

                    new(JwtRegisteredClaimNames.Email, user.Email),
                new(JwtRegisteredClaimNames.Name, user.Username),
                new(JwtRegisteredClaimNames.Aud, _configuration.GetSection("JWT").GetSection("ValidAudience").Value!),
                new(JwtRegisteredClaimNames.Iss, _configuration.GetSection("JWT").GetSection("ValidIssuer").Value!)
                ];
            claims.Add(new Claim(ClaimTypes.Role, role.UserDetail!));

            var tokenDecription = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)

            };

            var token = tokenHandel.CreateToken(tokenDecription);
            return tokenHandel.WriteToken(token);
        }

    }
}
