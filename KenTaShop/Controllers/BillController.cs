using KenTaShop.Services;
using KenTaShop.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KenTaShop.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class BillController : ControllerBase
    {
        private readonly IBillRepository _billRepo;

        public BillController(IBillRepository billRepo)
        {
            _billRepo = billRepo;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var bill = await _billRepo.GetAll();
            return Ok(bill);
        }

        [HttpPost("AddBill")]
        public async Task<IActionResult> AddBill([FromForm] addBill addbill)
        {
            var bill = await _billRepo.AddBill(addbill);
            return Ok(bill);
        }
        [HttpPut("EditByIdBill")]
        public async Task<IActionResult> EditByIdBill([FromForm] int idBill, [FromForm] addBill editbill)
        {
            return Ok(await _billRepo.EditByIdBill(idBill, editbill));
        }

        [HttpDelete("DeleByIdBill")]
        public async Task<IActionResult> DeleByIdBill([FromForm] int idBill)
        {
            return Ok(await _billRepo.DeleByIdBill(idBill));
        }
    }
}
