using KenTaShop.Data;
using KenTaShop.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KenTaShop.Services
{
    public interface IUserRepository
    {
        Task<JsonResult> AdminAdd(AdminAdd admiadd);
        Task<JsonResult> AddUser(InforUser inforuser);
        Task<JsonResult> DeleteById(int idUser);
        Task<JsonResult> EditUser(int idUser, InforUser infouser);
        Task<List<UserMD>> GetAll();
      

        public class UserRepository : IUserRepository
        {
            private readonly ClothesShopManagementContext _context;
            private readonly PasswordHasher passwordHasher ;
            private readonly ISendEmailRepository IsendEmailServicesRepo;

            public UserRepository(ClothesShopManagementContext context, ISendEmailRepository IsendEmailServicesRepo)
            {
                _context = context;
                passwordHasher = new PasswordHasher();
                this.IsendEmailServicesRepo = IsendEmailServicesRepo;
            }

            public async Task<JsonResult> AddUser(InforUser inforuser )
            {
                var add = await _context.Users.SingleOrDefaultAsync( a => a.Email == inforuser.Email);
                if (add == null)
                {

                    var user = new User
                    {
                        IdUsertype = inforuser.IdUsertype,
                        Email = inforuser.Email,
                        Username = inforuser.Username,
                        Pass = inforuser.Pass
                    };

                    _context.Users.Add(user);
                    _context.SaveChanges();
                    return new JsonResult("Thành công")
                    {
                        StatusCode = StatusCodes.Status201Created
                    };
                }
                
                else
                {
                    return new JsonResult("Đã tồn tai")
                    {
                        StatusCode = StatusCodes.Status500InternalServerError
                    };

                }
            }

            public async Task<JsonResult> AdminAdd(AdminAdd adminadd)
            {
                var user = await(_context.Users.SingleOrDefaultAsync(u => u.Email == adminadd.Email));
                if (user != null)
                {
                    return new JsonResult("tài khoản đã tồn tại")
                    {
                        StatusCode = StatusCodes.Status400BadRequest
                    };
                }
                var pass = passwordHasher.GetRandomPassword();
                Console.WriteLine(pass);
                var passhash = passwordHasher.HashPassword(pass);
                var accuser = new User
                {

                    Username = adminadd.Username,
                    Pass = passhash,
                    Email = adminadd.Email,
                    IdUsertype = 1
                };

                await _context.AddAsync(accuser);
                await _context.SaveChangesAsync();
                EmailModel emailModel = new EmailModel();
                emailModel.ToEmail = adminadd.Email;
                emailModel.Subject = "Chào bạn";
                emailModel.Body = $"tài khoản:{adminadd.Email} \n mật khẩu là {pass}";
                var kt = IsendEmailServicesRepo.SendEmail(emailModel);
                if (kt)
                    Console.WriteLine("gui mail thanh cong");
                else
                { Console.WriteLine("gui mail that bai"); }
                return new JsonResult("thêm tài khoản thành công ")
                {
                    StatusCode = StatusCodes.Status201Created
                };
            }

            public async Task<JsonResult> DeleteById(int idUser)
            {
                var checkexist = await _context.Users.SingleOrDefaultAsync(a => a.IdUser == idUser);
                if(checkexist == null)
                {
                    return new JsonResult("Ko tồn tai")
                    {
                        StatusCode = StatusCodes.Status404NotFound
                    };
                }
                else
                {
                    _context.Remove(checkexist);
                    _context.SaveChanges();
                    return new JsonResult("Đã xóa")
                    {
                        StatusCode = StatusCodes.Status200OK
                    };
                }
            }

            public async Task<JsonResult> EditUser(int idUser, InforUser inforuser)
            {
                var check = await _context.Users.SingleOrDefaultAsync(a => a.IdUser == idUser);
                if(check == null)
                {
                    return new JsonResult("ko tồn tại")
                    {
                        StatusCode = StatusCodes.Status404NotFound
                    };
                }
                else
                {
                    check.IdUsertype = inforuser.IdUsertype;
                    check.Email = inforuser.Email;
                    check.Username = inforuser.Username;
                    check.Pass = inforuser.Pass;

                    return new JsonResult("Thành công")
                    {
                        StatusCode = StatusCodes.Status201Created
                    };
                }
            }

            public async Task<List<UserMD>> GetAll()
            {
                var users = await _context.Users.Select(x => new UserMD
                {
                    IdUser = x.IdUser,
                    IdUsertype = x.IdUsertype,
                    Email = x.Email,
                    Username = x.Username,
                    Pass = x.Pass
                }).ToListAsync();

                return users;
            }
        }
    }
}
