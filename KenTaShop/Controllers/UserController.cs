using KenTaShop.Data;
using KenTaShop.Services;
using KenTaShop.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace KenTaShop.Controllers
{
    [Authorize(Roles ="Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IConfiguration _configuration; //cung cấp các phương pháp để truy xuất giá trị cấu hình từ các nguồn khác nhau như tệp cấu hình,
        private readonly IUserRepository _userRepo;
        private readonly ClothesShopManagementContext dbcontext;
        private readonly PasswordHasher passwordHasher;

        public UserController(IUserRepository userRepo, IConfiguration configuration)
        {
            _configuration = configuration;
            dbcontext = new ClothesShopManagementContext();
            passwordHasher = new PasswordHasher();
            _userRepo = userRepo;
        }
        [HttpGet("All")]
        public async Task<IActionResult> GetAll()
        {
            var users = await _userRepo.GetAll();
            return Ok(users);
        }

        [AllowAnonymous]
        [HttpGet("GetbyId/{getiduser}")]
        public async Task<IActionResult> GetByIdUser( int getiduser)
        {
            var users  = await _userRepo.GetByIdUser(getiduser);
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

        // login
        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult<AuthResposencs>> Login(Login login)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await (from l in dbcontext.Users where l.Email == login.Email select l).SingleOrDefaultAsync();
            if (user is null)
            {
                return Unauthorized(new AuthResposencs()
                {
                    IsSuccess = false,
                    Message = "User not found  with email"
                });
            }

            var res = passwordHasher.verifyPassword(login.Pass!, user.Pass!);
            if(!res)
            {
                return Unauthorized(new AuthResposencs()
                {
                    IsSuccess = false,
                    Message = "Error Password"
                });
            }
            var token = GenerateToken(user);
            return Ok(new AuthResposencs
            {
                Token = token,
                IsSuccess = true,
                Message = "Login Success"
            });
        }
        //tao token
        [NonAction]
        public string GenerateToken(User user)
        {
            // khởi tạo biến
            var tokenHandle = new JwtSecurityTokenHandler();

            var key = Encoding.UTF8.GetBytes(_configuration.GetSection("JWT").GetSection("Access_Secret").Value!);
            var role = (from r in dbcontext.Userstypes where r.IdUsertype == user.IdUsertype select r).SingleOrDefault();
            List<Claim> claims = [
                new(JwtRegisteredClaimNames.NameId, user.IdUser.ToString()),
                new (JwtRegisteredClaimNames.Email , user.Email),
                new (JwtRegisteredClaimNames.Name, user.Username),
                new (JwtRegisteredClaimNames.Aud, _configuration.GetSection("JWT").GetSection("ValidAudience").Value!),
                new (JwtRegisteredClaimNames.Iss, _configuration.GetSection("JWT").GetSection("ValidIssuer").Value!)
                ];
            claims.Add(new Claim(ClaimTypes.Role, role.UserDetail!));

            var tokenDescription = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
            };

            var token = tokenHandle.CreateToken(tokenDescription);
            return tokenHandle.WriteToken(token); 
        }
    }
}
