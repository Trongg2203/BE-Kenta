using KenTaShop.Data;
using KenTaShop.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KenTaShop.Services
{
    public interface IImportGoodsinforRepository
    {
        Task<JsonResult> AddImportGoodsinfor(ImportGoodsinforVM importGoodsinforVM);
        Task<JsonResult> DeleteImportGoodsinfor(int idimportGoodsinfor);
        Task<JsonResult> EditImportGoodsinfor(int idimportGoodsinfor, ImportGoodsinforVM importGoodsinforVM);
        Task<List<ImportGoodsinforMD>> GetAll();
        public Task<ImportGoodsinforMD> GetById(int id);
    }
    public class ImportGoodsinforRepository : IImportGoodsinforRepository
    {
        private readonly ClothesShopManagementContext _context;
        public ImportGoodsinforRepository(ClothesShopManagementContext context)
        {
            _context = context;
        }

        public async Task<JsonResult> AddImportGoodsinfor(ImportGoodsinforVM importGoodsinforVM)
        {
            var importgoodsinfor = new ImportGoodsinfor
            {
                IdGoods = importGoodsinforVM.IdGoods,
                Quantity = importGoodsinforVM.Quantity,
                Price = importGoodsinforVM.Price,
                IdProducer = importGoodsinforVM.IdProducer,
                Vat = importGoodsinforVM.Vat,
                Total = importGoodsinforVM.Total,

            };
            await _context.ImportGoodsinfors.AddAsync(importgoodsinfor);
            _context.SaveChanges();
            return new JsonResult("da khoi tao ")
            {
                StatusCode = StatusCodes.Status201Created
            };
        }

        public async Task<JsonResult> DeleteImportGoodsinfor(int idimportGoodsinfor)
        {
            var check = await _context.ImportGoodsinfors.SingleOrDefaultAsync(l => l.IdImportGoodsinfor == idimportGoodsinfor);
            if (check == null)
            {
                return new JsonResult("Chua tim thay de xoa")
                {
                    StatusCode = StatusCodes.Status404NotFound
                };
            }
            else
            {
                _context.ImportGoodsinfors.Remove(check);
                _context.SaveChanges();
                return new JsonResult("da xoa ")
                {
                    StatusCode = StatusCodes.Status200OK
                };
            }
        }

        public async Task<JsonResult> EditImportGoodsinfor(int idimportGoodsinfor, ImportGoodsinforVM importGoodsinforVM)
        {
            var importgoodsinfor = await _context.ImportGoodsinfors.SingleOrDefaultAsync(l => l.IdImportGoodsinfor == idimportGoodsinfor);
            if (importgoodsinfor == null)
            {
                return new JsonResult("khong tim thay loai can sua")
                {
                    StatusCode = StatusCodes.Status404NotFound
                };
            }
            else
            {
                importgoodsinfor.IdGoods = importGoodsinforVM.IdGoods;
                importgoodsinfor.Quantity = importGoodsinforVM.Quantity;
                importgoodsinfor.Price = importGoodsinforVM.Price;
                importgoodsinfor.IdProducer = importGoodsinforVM.IdProducer;
                importgoodsinfor.Vat = importGoodsinforVM.Vat;
                importgoodsinfor.Total = importGoodsinforVM.Total;

                _context.SaveChanges();
                return new JsonResult("da chinh sua")
                {
                    StatusCode = StatusCodes.Status200OK
                };
            }
        }

        public async Task<List<ImportGoodsinforMD>> GetAll()
        {
            var importgoodsinfor = await _context.ImportGoodsinfors.Select(u => new ImportGoodsinforMD
            {
                IdGoods = u.IdGoods,
                Quantity = u.Quantity,
                Price = u.Price,
                IdProducer = u.IdProducer,
                Vat = u.Vat,
                Total = u.Total,
            }).ToListAsync();
            return importgoodsinfor;
        }

        public async Task<ImportGoodsinforMD> GetById(int id)
        {
            var importgoodsinfor = await _context.ImportGoodsinfors.SingleOrDefaultAsync(h => h.IdImportGoodsinfor == id);
            if (importgoodsinfor is null)
                return null;
            return new ImportGoodsinforMD
            {
                IdImportGoodsinfor = importgoodsinfor.IdImportGoodsinfor,
                IdGoods = importgoodsinfor.IdGoods,
                Quantity = importgoodsinfor.Quantity,
                Price = importgoodsinfor.Price,
                IdProducer = importgoodsinfor.IdProducer,
                IdGoodsNavigation = importgoodsinfor.IdGoodsNavigation,
                IdProducerNavigation = importgoodsinfor.IdProducerNavigation,
                Vat = importgoodsinfor.Vat,
                Total = importgoodsinfor.Total,
            };
        }
    }
}
