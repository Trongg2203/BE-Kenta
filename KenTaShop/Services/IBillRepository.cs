using KenTaShop.Data;
using KenTaShop.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KenTaShop.Services
{
    public interface IBillRepository
    {
        Task <JsonResult> AddBill(addBill addbill);
        Task<JsonResult> DeleByIdBill(int idBill);
        Task<JsonResult> EditByIdBill(int idBill, addBill editbill);
        Task<List<BillMD>> GetAll();

        public class BillRepository : IBillRepository
        {
            private readonly ClothesShopManagementContext _context;

            public BillRepository(ClothesShopManagementContext context)
            {
                _context = context;
            }

            public async Task<JsonResult> AddBill(addBill addbill)
            {
                var bill = new Bill()
                {
                    IdUser = addbill.IdUser,
                    BillDate = addbill.BillDate,
                    BillTotal = addbill.BillTotal,
                    IdStatus = addbill.IdStatus,
                };
                await _context.Bills.AddAsync(bill);
                await _context.SaveChangesAsync();
                return new JsonResult("Đã thêm")
                {
                    StatusCode = StatusCodes.Status200OK
                };
            }

            public async Task<JsonResult> DeleByIdBill(int idBill)
            {
                var checkexist = await _context.Bills.SingleOrDefaultAsync(o => o.IdBill == idBill);
                if(checkexist == null)
                {
                    return new JsonResult("không tồn tại")
                    {
                        StatusCode = StatusCodes.Status404NotFound
                    };
                }
                else
                {
                   _context.Remove(checkexist);
                   await _context.SaveChangesAsync();
                    return new JsonResult("Deleted")
                    {
                        StatusCode = StatusCodes.Status200OK
                    };
                }
            }

            public async Task<JsonResult> EditByIdBill(int idBill, addBill editbill)
            {
                var check = await _context.Bills.SingleOrDefaultAsync(o => o.IdBill == idBill);
                if(check != null)
                {
                    check.IdUser = idBill;
                    check.BillTotal = editbill.BillTotal;
                    check.BillDate = editbill.BillDate;
                    check.IdStatus = editbill.IdStatus;

                    _context.SaveChanges();
                    return new JsonResult("Thành công")
                    {
                        StatusCode = StatusCodes.Status200OK
                    };
                }
                else
                {
                    return new JsonResult("k tồn tại")
                    {
                        StatusCode = StatusCodes.Status404NotFound
                    };
                }
                
            }

            public async Task<List<BillMD>> GetAll()
            {
                var bill = await _context.Bills.Select(o => new BillMD
                {
                    IdBill = o.IdBill,
                    IdUser = o.IdUser,
                    BillDate = o.BillDate,
                    BillTotal = o.BillTotal,
                    IdStatus = o.IdStatus,
                }).ToListAsync();
                return bill;
            }
        }
    }
}
