using KenTaShop.Services;
using KenTaShop.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;

namespace KenTaShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GoodsinforController : ControllerBase
    {
        private readonly IGoodsinforRepository _GoodsinforRepo;

        public GoodsinforController(IGoodsinforRepository GoodsinforRepo) 
        {
            _GoodsinforRepo = GoodsinforRepo;
        }
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var Goodsin = await _GoodsinforRepo.GetAll();
            return Ok(Goodsin);
        }
        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var Goodsin = await _GoodsinforRepo.GetById(id);
            return Ok(Goodsin);
        }
        [HttpPost("Add")]
        public async Task<IActionResult> Add(GoodsinforVM goo)
        {
            var Goodsin = await _GoodsinforRepo.Add(goo);
            return Ok(Goodsin);
        }
        [HttpPut("Edit")]
        public async Task<IActionResult> Edit(GoodsinforVM goo)
        {
            var Goodsin = await _GoodsinforRepo.Edit(goo);
            return Ok(Goodsin);
        }
        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete(int id)
        {
            var Goodsin = await _GoodsinforRepo.Delete(id);
            return Ok(Goodsin);
        }
        
    }
}
