using KenTaShop.Data;
using KenTaShop.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;

namespace KenTaShop.Services
{
    public interface IRatingRepository
    {
        Task<List<RatingVM>> GetAll();
        Task<JsonResult> Add(RatingVM rating);
        Task<RatingVM> GetById(int idbill, int idgood);
        Task<JsonResult> Edit(RatingVM rating);
        Task<JsonResult> Delete(RatingVM Rating);
    }
    public class RatingRepository:IRatingRepository
    {
        private readonly ClothesShopManagementContext _context;

        public RatingRepository(ClothesShopManagementContext context) 
        {
            _context = context;
        }

        public async Task<JsonResult> Add(RatingVM Rating)
        {
            var rating = await _context.Ratings.SingleOrDefaultAsync(s => s.IdBill == Rating.IdBill && s.IdGoods == Rating.IdGoods);
            if (rating == null)
            {
                var _Rating = new Rating
                {
                    IdBill = Rating.IdBill,
                    IdGoods = Rating.IdGoods,
                    Rating1 = Rating.Rating1,
                    Comment = Rating.Comment,
                    CreatedDate = Rating.CreatedDate,
                };
                await _context.AddAsync(_Rating);
                await _context.SaveChangesAsync();
                return new JsonResult("Đã thêm thành công")
                {
                    StatusCode = StatusCodes.Status200OK
                };
            }
            else
            {
                return new JsonResult("Thêm không thành công")
                {
                    StatusCode = StatusCodes.Status400BadRequest
                };
            }
            
        }

        public async Task<JsonResult> Delete(RatingVM Rating)
        {
            var rating = await _context.Ratings.SingleOrDefaultAsync(s => s.IdBill == Rating.IdBill && s.IdGoods == Rating.IdGoods);
            if (rating == null)
            {
                return new JsonResult("Không tìm thấy")
                {
                    StatusCode = StatusCodes.Status400BadRequest
                };
            }
            else
            {
                _context.Remove(rating);
                await _context.SaveChangesAsync();
                return new JsonResult("Xoá thành công")
                {
                    StatusCode = StatusCodes.Status200OK
                };
            }
        }

        public async Task<JsonResult> Edit(RatingVM Rating)
        {
            var rating = await _context.Ratings.SingleOrDefaultAsync(s => s.IdBill == Rating.IdBill && s.IdGoods == Rating.IdGoods);
            if (rating == null)
            {
                return new JsonResult("Không tìm thấy")
                {
                    StatusCode = StatusCodes.Status400BadRequest
                };
            }
            else
            {
                rating.Rating1 = Rating.Rating1;
                rating.Comment = Rating.Comment;
                rating.CreatedDate = Rating.CreatedDate;
                await _context.SaveChangesAsync();
                return new JsonResult("Sửa thành công")
                {
                    StatusCode = StatusCodes.Status200OK
                };
            }
        }

        public async Task<List<RatingVM>> GetAll()
        {
            var rating = await _context.Ratings.Select(s => new RatingVM
            {
                IdGoods=s.IdGoods,
                IdBill=s.IdBill,
                Rating1 = s.Rating1,
                Comment = s.Comment,
                CreatedDate=s.CreatedDate,
            }).ToListAsync();
            return rating;
        }

        public async Task<RatingVM> GetById(int idbill, int idgood)
        {
            var rating = await _context.Ratings.SingleOrDefaultAsync(s => s.IdBill == idbill && s.IdGoods == idgood);
            if (rating == null)
            {
                return null;
            }
            else
            {
                return new RatingVM
                {
                    IdGoods = rating.IdGoods,
                    IdBill = rating.IdBill,
                    Rating1 = rating.Rating1,
                    Comment = rating.Comment,
                    CreatedDate = rating.CreatedDate,
                };
            }
        }
    }

}
