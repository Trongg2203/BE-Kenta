using KenTaShop.Data;
using KenTaShop.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KenTaShop.Services
{
    public interface IGoodsinforRepository
    {
        Task<List<GoodsinforVM>> GetAll();
        Task<GoodsinforMD> GetById(int id);
        Task<JsonResult> Add(GoodsinforVM Goodsin);
        Task<JsonResult> Edit(GoodsinforVM goo);
        Task<JsonResult> Delete(int id);
    }
    public class GoodsinforRepository:IGoodsinforRepository
    {
        private readonly ClothesShopManagementContext _context;

        public GoodsinforRepository(ClothesShopManagementContext context)
        {
            _context = context;
        }

        public async Task<JsonResult> Add(GoodsinforVM Goodsin)
        {
            var Goods = await _context.Goodsinfors.SingleOrDefaultAsync(s => s.IdGoods == Goodsin.IdGoods);
            if (Goods == null)
            {
                var _Good = new Goodsinfor
                {
                    IdGoods = Goodsin.IdGoods,
                    GoodsDetail = Goodsin.GoodsDetail,
                    Size = Goodsin.Size,
                    Color = Goodsin.Color,
                };
                await _context.AddAsync(_Good);
                await _context.SaveChangesAsync();
                return new JsonResult("Đã thêm thành công")
                {
                    StatusCode =StatusCodes.Status200OK
                };
            }
            else
            {
                return new JsonResult("Đã có thông tin sản phẩm này")
                {
                    StatusCode = StatusCodes.Status400BadRequest
                };
            }
        }

        public async Task<JsonResult> Delete(int id)
        {
            var Goods = await _context.Goodsinfors.SingleOrDefaultAsync(s => s.IdGoods == id);
            if(Goods == null)
            {
                return new JsonResult("Không tìm thấy")
                {
                    StatusCode = StatusCodes.Status400BadRequest
                };
            }
            else
            {
                _context.Remove(Goods);
                await _context.SaveChangesAsync();
                return new JsonResult("Đã xoá thành công")
                {
                    StatusCode = StatusCodes.Status200OK
                };
            }
        }

        public async Task<JsonResult> Edit(GoodsinforVM goo)
        {
            var Goods = await _context.Goodsinfors.SingleOrDefaultAsync(s => s.IdGoods == goo.IdGoods);
            if(Goods == null)
            {
                return new JsonResult("Không tìm thấy")
                {
                    StatusCode = StatusCodes.Status400BadRequest
                };
            }
            else
            {
                Goods.IdGoods=goo.IdGoods;
                Goods.GoodsDetail= goo.GoodsDetail;
                Goods.Color = goo.Color;
                Goods.Size = goo.Size;
                await _context.SaveChangesAsync(true);
                return new JsonResult("Đã sửa thành công")
                {
                    StatusCode = StatusCodes.Status200OK
                };
            }
        }

        public async Task<List<GoodsinforVM>> GetAll()
        {
            var Goods = await _context.Goodsinfors.Select(s => new GoodsinforVM
            {
                IdGoods = s.IdGoods,
                GoodsDetail = s.GoodsDetail,
                Color = s.Color,
                Size = s.Size,
            }).ToListAsync();
            return Goods;
        }

        public async Task<GoodsinforMD> GetById(int id)
        {
            var Goods=await _context.Goodsinfors.SingleOrDefaultAsync(s=>s.IdGoods == id);
            if (Goods == null)
            {
                return null;
            }
            else
            {
                return new GoodsinforMD { IdGoods = Goods.IdGoods,
                    GoodsDetail = Goods.GoodsDetail, Color = Goods.Color, Size = Goods.Size,
                    IdGoodsInfor = Goods.IdGoodsInfor,IdGoodsNavigation = Goods.IdGoodsNavigation
     
                };

            }
        }
    }
}
