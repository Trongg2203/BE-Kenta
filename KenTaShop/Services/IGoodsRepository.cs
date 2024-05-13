using KenTaShop.Data;
using KenTaShop.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KenTaShop.Services
{
    public interface IGoodsRepository
    {
        Task<List<HinhAnhSp>> GetAll(QueryProductinPage query);
        Task<GoodDetail> GetById(int id);
        Task<JsonResult> Add(GoodsVM goodsVM);
        Task<JsonResult?> Edit(GoodsMD good);
        Task<IActionResult?> Delete(int id);
        Task<JsonResult?> AddPic(Goodpic idpic, List<IFormFile> files);
        Task<List<GetByCate>> GetByCate(int idgoodstype);
    }
    public class GoodsRepository : IGoodsRepository
    {
        private readonly ClothesShopManagementContext _context;
        private readonly IPictureRepository _PictureRepo;

        public GoodsRepository(ClothesShopManagementContext context,IPictureRepository PictureRepo)
        {
            _context = context;
            _PictureRepo = PictureRepo;
        }

        public async Task<JsonResult> Add(GoodsVM goodsVM)
        {
                var _Good = new Good
                {
                    //IdGoods = goodsVM.IdGoods,
                    IdGoodstype = goodsVM.IdGoodstype,
                    GoodsName = goodsVM.GoodsName,
                    Quantity = goodsVM.Quantity,
                    GoodsPrice = goodsVM.GoodsPrice
                };
                await _context.Goods.AddAsync(_Good);
                await _context.SaveChangesAsync();
                return new JsonResult("Đã thêm ")
                {
                    StatusCode = StatusCodes.Status200OK
                };
            
        }

        public async Task<JsonResult?> AddPic(Goodpic idpic, List<IFormFile> files)
        {
            int idGoods = idpic.IdGoods;
            string folder = "Goods";
            List<string> images = await _PictureRepo.WriteFileAsync(files, folder);
            if (images.Count != 0)
            {
                foreach (string image in images)
                {
                    var item = new Picture()
                    {
                        IdGoods=idGoods,
                        Url = image

                    };
                    _context.Pictures.Add(item);
                }
            }
            _context.SaveChanges(); 
            return new JsonResult("thanh cong")
            {

            };
        }

        public async  Task<IActionResult?> Delete(int id)
        {
            var Goods = await _context.Goods.SingleOrDefaultAsync(s => s.IdGoods == id);
            if (Goods != null)
            {
                _context.Remove(Goods);
                await _context.SaveChangesAsync();
                return new JsonResult("Đã xoá")
                {
                    StatusCode = StatusCodes.Status200OK
                };
            }
            else            {
                return new JsonResult("Không tìm thấy")
                {
                    StatusCode = StatusCodes.Status400BadRequest
                };
            }
        }

        public async Task<JsonResult?> Edit(GoodsMD good)
        {
            var Goods=await _context.Goods.SingleOrDefaultAsync(s=>s.IdGoods == good.IdGoods);
            if(Goods == null)
            {
                return new JsonResult("Không có hàng hoá này")
                {
                    StatusCode = StatusCodes.Status400BadRequest
                };
            }
            else
            {
                //Goods.IdGoods = good.IdGoods;
                Goods.GoodsName = good.GoodsName;
                Goods.IdGoodstype = good.IdGoodstype;
                Goods.Quantity = good.Quantity;
                Goods.GoodsPrice = good.GoodsPrice;
                await _context.SaveChangesAsync();
                return new JsonResult("Đã sửa ")
                {
                    StatusCode=StatusCodes.Status200OK
                };
            }
        }

        public async Task<List<HinhAnhSp>> GetAll(QueryProductinPage query)
        {
            query.SoSpinTrang = 12;
            var Goods = await _context.Goods.Include(u => u.Pictures).Select(s => new HinhAnhSp
            {
                IdGoods = s.IdGoods,
                GoodsName = s.GoodsName,
                IdGoodstype = s.IdGoodstype,
                Quantity = s.Quantity,
                GoodsPrice = s.GoodsPrice,
                Pictures = s.Pictures.Select(a => new HinhAnhSanPham
                {
                    Url = a.Url,
                }).ToList()
            }).ToListAsync();

            var skip = (query.SoTrang - 1) * (query.SoSpinTrang);
            return Goods.Skip(skip).Take(query.SoSpinTrang).ToList();
        }

        public async Task<List<GetByCate>> GetByCate(int idgoodstype)
        {
            var products = await (from b in _context.Goods
                                  where b.IdGoodstype == idgoodstype
                                  select new GetByCate
                                  {
                                      GoodsName = b.GoodsName,
                                      IdGoodstype = b.IdGoodstype,
                                      GoodsPrice = b.GoodsPrice,
                                      IdGoods = b.IdGoods,
                                      Pictures = b.Pictures,
                                      Quantity = b.Quantity

                                  }).ToListAsync();
            return products;
        }

        public async Task<GoodDetail> GetById(int id)
        {
            var Goods=await _context.Goods.Include( u => u.Pictures).Include(a=> a.Goodsinfors).SingleOrDefaultAsync(s=>s.IdGoods==id);
            if (Goods==null)
            {
                return null;
            }
            else
            {
                return new GoodDetail {
                    IdGoods = Goods.IdGoods,
                    GoodsName = Goods.GoodsName,
                    GoodsPrice = Goods.GoodsPrice,
                    IdGoodstype = Goods.IdGoodstype,
                    Quantity = Goods.Quantity,
                    HinhSanPham = Goods.Pictures.Select(a => new HinhAnhSanPham
                    {
                        Url = a.Url,
                    }).ToList(),
                    detailgoodinfor = Goods.Goodsinfors.Select(b => new DetailGoodinfor
                    {
                        GoodsDetail = b.GoodsDetail,
                        Size = b.Size,
                        Color = b.Color,
                    }).ToList(),
                };
            }
        }
    }
}
