using KenTaShop.Data;
using KenTaShop.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KenTaShop.Services
{
    public interface IStatusRepository
    {
        Task<List<StatusVM>> GetAll();
        Task<StatusVM> GetById(int id);
        Task<JsonResult> Add(string sta);
        Task<JsonResult> Edit(StatusVM sta);
        Task<JsonResult> Delete(int id);
    }
    public class StatusRepository : IStatusRepository
    {
        private readonly ClothesShopManagementContext _context;

        public StatusRepository(ClothesShopManagementContext context)
        {
            _context = context;
        }

        public async Task<JsonResult> Add(string sta)
        {
            var status = await _context.Statuses.SingleOrDefaultAsync(s => s.Statusdetail==sta);
            if (status == null)
            {
                var _status = new Status
                {
                    Statusdetail = sta,
                };
                await _context.AddAsync(_status);
                await _context.SaveChangesAsync();
                return new JsonResult("Đã thêm thành công")
                {
                    StatusCode = StatusCodes.Status200OK
                };
            }
            else
            {
                return new JsonResult("Đã có id này")
                {
                    StatusCode = StatusCodes.Status400BadRequest
                };
            }

        }

        public async Task<JsonResult> Delete(int id)
        {
            var status = await _context.Statuses.SingleOrDefaultAsync(s => s.IdStatus == id);
            if (status == null)
            {
                return new JsonResult("không tìm thấy")
                {
                    StatusCode = StatusCodes.Status400BadRequest
                };
            }
            else
            {
                _context.Remove(status);
                await _context.SaveChangesAsync();
                return new JsonResult("Đã xoá thành công")
                {
                    StatusCode = StatusCodes.Status200OK
                };
            }
        }

        public async Task<JsonResult> Edit(StatusVM sta)
        {
            var status = await _context.Statuses.SingleOrDefaultAsync(s => s.IdStatus == sta.IdStatus);
            if (status != null)
            {
                status.Statusdetail = sta.Statusdetail;
                await _context.SaveChangesAsync();
                return new JsonResult("Đã sửa thành công")
                {
                    StatusCode = StatusCodes.Status200OK
                };
            }
            else
            {
                return new JsonResult("không tìm thấy")
                {
                    StatusCode = StatusCodes.Status400BadRequest
                };
            }
        }

        public async Task<List<StatusVM>> GetAll()
        {
            var status = await _context.Statuses.Select(s => new StatusVM
            {
                IdStatus = s.IdStatus,
                Statusdetail = s.Statusdetail,
            })
            .ToListAsync();
            return status;
        }

        public async Task<StatusVM> GetById(int id)
        {
            var status = await _context.Statuses.SingleOrDefaultAsync(s => s.IdStatus == id);
            if (status == null)
            {
                return null;
            }
            else
            {
                return new StatusVM
                {
                    IdStatus = status.IdStatus,
                    Statusdetail = status.Statusdetail,
                };
            }
        }
    }
}

