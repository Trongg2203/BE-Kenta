using KenTaShop.Data;
using KenTaShop.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KenTaShop.Services
{
    public interface IUserRepository
    {
        Task<JsonResult> AddUser(InforUser inforuser);
        Task<JsonResult> DeleteById(int idUser);
        Task<JsonResult> EditUser(int idUser, InforUser infouser);
        Task<List<UserMD>> GetAll();
      

        public class UserRepository : IUserRepository
        {
            private readonly ClothesShopManagementContext _context;

            public UserRepository(ClothesShopManagementContext context)
            {
                _context = context;
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
