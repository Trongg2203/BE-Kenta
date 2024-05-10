using KenTaShop.Data;
using KenTaShop.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KenTaShop.Services
{
    public interface IGoodstypeRepository
    {
        Task<JsonResult> AddGoodstype(GoodstypeVM goodstypeVM);
        Task<JsonResult> DeleteGoodstype(int idgoodstype);
        Task<JsonResult> EditGoodstype(int idgoodstype, GoodstypeVM goodstypeVM);
        Task<List<GoodstypeMD>> GetAll();
        public Task<GetDetail> GetById(int id);
    }
    public class GoodstypeRepository : IGoodstypeRepository
    {
        private readonly ClothesShopManagementContext _context;
        public GoodstypeRepository(ClothesShopManagementContext context)
        {
            _context = context;
        }

        public async Task<JsonResult> AddGoodstype(GoodstypeVM goodstypeVM)
        {
            var goodstype = new Goodstype
            {
                GoodstypeDetail = goodstypeVM.GoodstypeDetail,
                Displayorder = goodstypeVM.Displayorder,
                FatherFolder = goodstypeVM.FatherFolder,
                SonFolder = goodstypeVM.SonFolder,

            };
            await _context.Goodstypes.AddAsync(goodstype);
            _context.SaveChanges();
            return new JsonResult("da khoi tao ")
            {
                StatusCode = StatusCodes.Status201Created
            };
        }

        public async Task<JsonResult> DeleteGoodstype(int idgoodstype)
        {
            var check = await _context.Goodstypes.SingleOrDefaultAsync(l => l.IdGoodstype == idgoodstype);
            if (check == null)
            {
                return new JsonResult("Chua tim thay de xoa")
                {
                    StatusCode = StatusCodes.Status404NotFound
                };
            }
            else
            {
                _context.Goodstypes.Remove(check);
                _context.SaveChanges();
                return new JsonResult("da xoa ")
                {
                    StatusCode = StatusCodes.Status200OK
                };
            }
        }

        public async Task<JsonResult> EditGoodstype(int idgoodstype, GoodstypeVM goodstypeVM)
        {
            var goodstype = await _context.Goodstypes.SingleOrDefaultAsync(l => l.IdGoodstype == idgoodstype);
            if (goodstype == null)
            {
                return new JsonResult("khong tim thay loai can sua")
                {
                    StatusCode = StatusCodes.Status404NotFound
                };
            }
            else
            {
                goodstype.GoodstypeDetail = goodstypeVM.GoodstypeDetail;
                goodstype.Displayorder = goodstypeVM.Displayorder;
                goodstype.FatherFolder = goodstypeVM.FatherFolder;
                goodstype.SonFolder = goodstypeVM.SonFolder;
                _context.SaveChanges();
                return new JsonResult("da chinh sua")
                {
                    StatusCode = StatusCodes.Status200OK
                };
            }
        }

        public async Task<List<GoodstypeMD>> GetAll()
        {
            var goodstype = await _context.Goodstypes.Select(u => new GoodstypeMD
            {
                IdGoodstype = u.IdGoodstype,
                GoodstypeDetail = u.GoodstypeDetail,
                Displayorder = u.Displayorder,
                FatherFolder = u.FatherFolder,
                SonFolder = u.SonFolder,

            }).ToListAsync();
            return goodstype;
        }

        public async Task<GetDetail> GetById(int id)
        {
            var goodstype = await _context.Goodstypes.Include(u => u.Goods).SingleOrDefaultAsync(h => h.IdGoodstype == id);
            if (goodstype is null)
                return null;
            return new GetDetail
            {

                IdGoodstype = goodstype.IdGoodstype,
                GoodstypeDetail = goodstype.GoodstypeDetail,
                Displayorder = goodstype.Displayorder,
                FatherFolder = goodstype.FatherFolder,
                SonFolder = goodstype.SonFolder,
                Goods = goodstype.Goods.Select(u => new GoodsVM
                {
                    IdGoodstype = u.IdGoodstype,
                    GoodsName = u.GoodsName,
                    GoodsPrice = u.GoodsPrice,
                    Quantity = u.Quantity,
                }).ToList()
            };
        }
    }
}
