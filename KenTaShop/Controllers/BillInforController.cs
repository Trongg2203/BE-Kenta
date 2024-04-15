using KenTaShop.Services;
using KenTaShop.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KenTaShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BillInforController : ControllerBase
    {
        private readonly IBillInforRepository _inforBillRepo;

        public BillInforController(IBillInforRepository billInforRepo) 
        {
            _inforBillRepo = billInforRepo;
        }

        [HttpGet("All")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _inforBillRepo.GetAll());
        }

        [HttpPost("AddBillInfor")]
        public async Task<IActionResult> AddBillInfor([FromForm] AddBillInfor billinformd)
        {
            return Ok(await _inforBillRepo.AddBillInfor(billinformd));
        }

        [HttpPut("Edit")]
        public async Task<IActionResult> Edit( [FromForm] AddBillInfor editbillinfor)
        {
            return Ok(await _inforBillRepo.Edit( editbillinfor));
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromForm] BillInforMD delete)
        {
            return Ok(await _inforBillRepo.Delete(delete));
        }
    }
}
