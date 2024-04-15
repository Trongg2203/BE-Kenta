using KenTaShop.Data;
using KenTaShop.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KenTaShop.Services
{
    public interface IUserTypeRepositoty
    {
        Task<JsonResult> AddUserType(UserTypeVM usertypevm);
        Task <JsonResult> DeleteUserType(int idUserType);
        Task<JsonResult> EditUserTypeById(int idUsertype, EditUserTypeMD edit);
        Task<List<UserTypeMD>> GetAll();
        Task<UserTypeMD> GetById(int idUsertype);
    }
    public class UserTypeRepositoty: IUserTypeRepositoty
    {
        private readonly ClothesShopManagementContext _context;

        public UserTypeRepositoty (ClothesShopManagementContext dbcontext)
        {
            _context = dbcontext;
        }

        public async Task<JsonResult> AddUserType(UserTypeVM usertypevm)
        {
            var check = await _context.Userstypes.SingleOrDefaultAsync(o => o.UserDetail == usertypevm.UserDetail);
            if (check == null) 
            {
                var loais = new Userstype
                {
                    UserDetail = usertypevm.UserDetail,
                };

                await _context.Userstypes.AddAsync(loais);
                await _context.SaveChangesAsync();

                return new JsonResult("Tạo thành công loại User")
                {
                    StatusCode = StatusCodes.Status201Created
                };
            }else
            {
                return new JsonResult("Đã tồn tại")
                {
                    StatusCode = StatusCodes.Status500InternalServerError
                };
            }

            
        }

        public async Task<JsonResult> DeleteUserType(int idUserType)
        {
            var checkexist = await _context.Userstypes.SingleOrDefaultAsync(o => o.IdUsertype == idUserType);
            if(checkexist == null)
            {
                return new JsonResult("id ko tồn tại")
                {
                    StatusCode = StatusCodes.Status404NotFound
                };
            }else
            {
                _context.Remove(checkexist);
                _context.SaveChanges();

                return new JsonResult("Đã xóa")
                {
                    StatusCode = StatusCodes.Status200OK
                };
            }
        }

        public async Task<JsonResult> EditUserTypeById(int idUsertype, EditUserTypeMD edit)
        {
            var loai = await _context.Userstypes.SingleOrDefaultAsync(o => o.IdUsertype == idUsertype);
            if(loai == null)
            {
                return new JsonResult("không tồn tại idUsertype")
                {
                    StatusCode = StatusCodes.Status404NotFound
                };
            }else
            {
                loai.UserDetail = edit.UserDetail;
                return new JsonResult("Đã chỉnh sửa")
                {
                    StatusCode = StatusCodes.Status200OK
                };
            }

        }

        public async Task<List<UserTypeMD>> GetAll()
        {
          var loais = await _context.Userstypes.Select(o => new UserTypeMD
            {
                IdUsertype = o.IdUsertype,
                UserDetail = o.UserDetail,
            }).ToListAsync();
            return loais;
        }


        public async Task<UserTypeMD> GetById(int idUsertype)
        {
            var loais = await _context.Userstypes.FirstOrDefaultAsync(o =>  o.IdUsertype == idUsertype);
            return new UserTypeMD { IdUsertype =  loais.IdUsertype, UserDetail = loais.UserDetail };
        }

        
    }
}
