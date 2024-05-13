 using KenTaShop.Data;
using KenTaShop.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Win32;

namespace KenTaShop.Services
{
    public interface IUserRepository
    {
        Task<JsonResult> AdminAdd(AdminAdd admiadd);
        Task<JsonResult> AddUser(InforUser inforuser);
        Task<JsonResult> DeleteById(int idUser);
        Task<JsonResult> EditUser(int idUser, InforUser infouser);
        Task<List<UserMD>> GetAll();
        Task<JsonResult> Register(Register register);
        Task<JsonResult> ResetPass(int id);
        Task<JsonResult?> ChangePass(ChangePass changePass);
        Task<GetByIdUser> GetByIdUser(int getiduser);

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

            public async Task<JsonResult> ResetPass(int id)
            {
                var check = await _context.Users.SingleOrDefaultAsync(a => a.IdUser == id);
                if(check == null)
                {
                    return new JsonResult("Không tìm thấy người dùng")
                    {
                        StatusCode = StatusCodes.Status400BadRequest
                    };
                }
                else
                {
                    var pass = passwordHasher.GetRandomPassword();
                    Console.WriteLine(pass);
                    var hashpass=passwordHasher.HashPassword(pass);
                    check.Pass = hashpass;
                    await _context.SaveChangesAsync();
                    EmailModel emailModel = new EmailModel();
                    emailModel.ToEmail = check.Email;
                    emailModel.Subject = "Chào bạn";
                    emailModel.Body = $"tài khoản:{check.Email} \n mật khẩu mới là {pass}";
                    var kt = IsendEmailServicesRepo.SendEmail(emailModel);
                    return new JsonResult("Đã reset pass")
                    {
                        StatusCode=StatusCodes.Status200OK
                    };

                }
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

            public async Task<JsonResult> Register(Register register)
            {
                var check =await _context.Users.SingleOrDefaultAsync(a=>a.Username == register.Username);
                if (check==null)
                {
                    var passhash = passwordHasher.HashPassword(register.Pass);
                    var accuser = new User
                    {

                        Username = register.Username,
                        Pass = passhash,
                        Email = register.Email,
                        IdUsertype = 2

                    };
                    await _context.AddAsync(accuser);
                    await _context.SaveChangesAsync();
                    EmailModel emailModel = new EmailModel();
                    emailModel.ToEmail = register.Email;
                    emailModel.Subject = "Chào bạn";
                    emailModel.Body = $"Tạo thành công tài khoản: {register.Email} \n với mật khẩu là {register.Pass}";
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
                else
                {
                    return new JsonResult("Đã có tên người dùng này ")
                    {
                        StatusCode = StatusCodes.Status400BadRequest
                    };
                }
            }

            public async Task<JsonResult?> ChangePass(ChangePass changePass)
            {
                var check =await _context.Users.SingleOrDefaultAsync(a=>a.Email==changePass.Email);
                if(check is null)
                {
                    return new JsonResult("Không tìm thấy người dùng")
                    {
                        StatusCode = StatusCodes.Status400BadRequest
                    };
                }
                else
                {
                    if (passwordHasher.verifyPassword(changePass.oldPass,check.Pass))
                    {
                        if (changePass.NewPass == changePass.ReNewPass)
                        {
                            if (changePass.NewPass != check.Pass)
                            {
                                var hashpass = passwordHasher.HashPassword(changePass.NewPass);
                                check.Pass = hashpass;
                                await _context.SaveChangesAsync();
                                EmailModel emailModel = new EmailModel();
                                emailModel.ToEmail = check.Email;
                                emailModel.Subject = "Chào bạn";
                                emailModel.Body = $"Tạo thành công tài khoản: {check.Email} \n với mật khẩu là {changePass.NewPass}";
                                var kt = IsendEmailServicesRepo.SendEmail(emailModel);
                                return new JsonResult("Đã thay đổi pass")
                                {
                                    StatusCode = StatusCodes.Status200OK
                                };
                            }
                            else return new JsonResult("Mật khẩu mới phải khác mật khẩu cũ")
                            {
                                StatusCode = StatusCodes.Status400BadRequest
                            };
                        }
                        else return new JsonResult("Mật khẩu mới nhập 2 lầm không giống nhau")
                        {
                            StatusCode = StatusCodes.Status400BadRequest
                        };
                    }
                    else
                    {
                        return new JsonResult("Mật khẩu không đúng")
                        {
                            StatusCode = StatusCodes.Status400BadRequest
                        };
                    }
                }
            }

            public async Task<GetByIdUser> GetByIdUser(int getiduser)
            {
                var Users = await _context.Users.SingleOrDefaultAsync(s => s.IdUser == getiduser);
                if(Users == null)
                {
                    return null;
                }
                else
                {
                    return new GetByIdUser
                    {
                        IdUser = Users.IdUser,
                        Username = Users.Username,
                        Email = Users.Email,
                        Location = Users.Location,
                        PhoneNumber = Users.PhoneNumber,
                    };
                }
            }
        }
    }
}
