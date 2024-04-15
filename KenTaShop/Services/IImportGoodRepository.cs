using KenTaShop.Data;
using KenTaShop.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KenTaShop.Services
{
    public interface IImportGoodRepository
    {
        Task<JsonResult> AddImportGood(ImportGoodVM importGoodVM);
        Task<JsonResult> DeleteImportGood(int idimportGood);
        Task<JsonResult> EditImportGood(int idimportGood, ImportGoodVM importGoodVM);
        Task<List<ImportGoodMD>> GetAll();
        public Task<ImportGoodMD> GetById(int id);
    }
    public class ImportGoodRepository : IImportGoodRepository
    {
        private readonly ClothesShopManagementContext _context;
        public ImportGoodRepository(ClothesShopManagementContext context)
        {
            _context = context;
        }

        public async Task<JsonResult> AddImportGood(ImportGoodVM importGoodVM)
        {
            var importgood = new ImportGood
            {
                IdGoodstype = importGoodVM.IdGoodstype,
                IdImportGoodsinfor = importGoodVM.IdImportGoodsinfor,
                CreatedDate = importGoodVM.CreatedDate,
                ImportTotal = importGoodVM.ImportTotal,
                Idstatus = importGoodVM.Idstatus,

            };
            await _context.ImportGoods.AddAsync(importgood);
            _context.SaveChanges();
            return new JsonResult("da khoi tao ")
            {
                StatusCode = StatusCodes.Status201Created
            };
        }

        public async Task<JsonResult> DeleteImportGood(int idimportGood)
        {
            var check = await _context.ImportGoods.SingleOrDefaultAsync(l => l.IdImport == idimportGood);
            if (check == null)
            {
                return new JsonResult("Chua tim thay de xoa")
                {
                    StatusCode = StatusCodes.Status404NotFound
                };
            }
            else
            {
                _context.ImportGoods.Remove(check);
                _context.SaveChanges();
                return new JsonResult("da xoa ")
                {
                    StatusCode = StatusCodes.Status200OK
                };
            }
        }

        public async Task<JsonResult> EditImportGood(int idimportGood, ImportGoodVM importGoodVM)
        {
            var importgood = await _context.ImportGoods.SingleOrDefaultAsync(l => l.IdImport == idimportGood);
            if (importgood == null)
            {
                return new JsonResult("khong tim thay loai can sua")
                {
                    StatusCode = StatusCodes.Status404NotFound
                };
            }
            else
            {
                importgood.IdGoodstype = importGoodVM.IdGoodstype;
                importgood.IdImportGoodsinfor = importGoodVM.IdImportGoodsinfor;
                importgood.CreatedDate = importGoodVM.CreatedDate;
                importgood.ImportTotal = importGoodVM.ImportTotal;
                importgood.Idstatus = importGoodVM.Idstatus;


                _context.SaveChanges();
                return new JsonResult("da chinh sua")
                {
                    StatusCode = StatusCodes.Status200OK
                };
            }
        }

        public async Task<List<ImportGoodMD>> GetAll()
        {
            var importgood = await _context.ImportGoods.Select(u => new ImportGoodMD
            {
                IdGoodstype = u.IdGoodstype,
                IdImportGoodsinfor = u.IdImportGoodsinfor,
                CreatedDate = u.CreatedDate,
                ImportTotal = u.ImportTotal,
                Idstatus = u.Idstatus,

            }).ToListAsync();
            return importgood;
        }

        public async Task<ImportGoodMD> GetById(int id)
        {
            var importgood = await _context.ImportGoods.SingleOrDefaultAsync(h => h.IdImport == id);
            if (importgood is null)
                return null;
            return new ImportGoodMD
            {
                IdImport = importgood.IdImport,
                IdGoodstype = importgood.IdGoodstype,
                IdImportGoodsinfor = importgood.IdImportGoodsinfor,
                CreatedDate = importgood.CreatedDate,
                ImportTotal = importgood.ImportTotal,
                Idstatus = importgood.Idstatus,
                IdGoodstypeNavigation = importgood.IdGoodstypeNavigation,
                IdImportGoodsinforNavigation = importgood.IdImportGoodsinforNavigation,
                IdstatusNavigation = importgood.IdstatusNavigation,
            };
        }
    }
}
