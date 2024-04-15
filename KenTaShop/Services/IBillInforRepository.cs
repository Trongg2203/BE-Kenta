using KenTaShop.Data;
using KenTaShop.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KenTaShop.Services
{
    public interface IBillInforRepository
    {
        Task<JsonResult> AddBillInfor(AddBillInfor billinformd);
        Task<JsonResult> Delete(BillInforMD delete);
        Task<JsonResult> Edit( AddBillInfor editbillinfor);
        Task<List<BillInforMD>> GetAll();

        public class BillInforRepository : IBillInforRepository
        {
            private readonly ClothesShopManagementContext _context;

            public BillInforRepository(ClothesShopManagementContext context)
            {
                _context = context;
            }

            public async Task<JsonResult> AddBillInfor(AddBillInfor billinformd)
            {
                var add = new Billinfor
                {
                    Idbill = billinformd.Idbill,
                    //IdGoods = billinformd.IdGoods,
                    Quantity = billinformd.Quantity,
                    Total = billinformd.Total,
                };
                await _context.AddAsync(add);
                await _context.SaveChangesAsync();

                return new JsonResult("Thành công")
                {
                    StatusCode = StatusCodes.Status201Created
                };
            }

            public async Task<JsonResult> Delete(BillInforMD delete)
            {
                var checkexist = await _context.Billinfors.SingleOrDefaultAsync(a => a.Idbill == delete.Idbill && a.IdGoods == delete.IdGoods);
                if (checkexist == null)
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

            public async Task<JsonResult> Edit( AddBillInfor editbillinfor)
            {
                var check = await _context.Billinfors.SingleOrDefaultAsync(a=> a.Idbill == editbillinfor.Idbill && a.IdGoods ==editbillinfor.IdGoods);
                if(check == null)
                {
                    return new JsonResult("khong tồn tại")
                    {
                        StatusCode = StatusCodes.Status404NotFound
                    };
                }
                else
                {
                    check.Idbill = editbillinfor.Idbill;
                    
                    check.Quantity = editbillinfor.Quantity;
                    check.Total = editbillinfor.Total;

                    await _context.SaveChangesAsync();
                    return new JsonResult("Đã sửa")
                    {
                        StatusCode = StatusCodes.Status200OK
                    };
                }
                    
            }

            public async Task<List<BillInforMD>> GetAll()
            {
                var billinfor = await _context.Billinfors.Select(o => new BillInforMD
                {
                    Idbill = o.Idbill,
                    IdGoods = o.IdGoods,
                    Quantity = o.Quantity,
                    Total = o.Total,
                }).ToListAsync();

                return billinfor;
            }
        }
    }
}
