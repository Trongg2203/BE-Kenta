using KenTaShop.Services;
using KenTaShop.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KenTaShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GoodsController : ControllerBase
    {
        private readonly IGoodsRepository _GoodsRepo;

        public GoodsController(IGoodsRepository GoodsRepo)
        {
            _GoodsRepo = GoodsRepo;
        }
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll([FromQuery] QueryProductinPage query)
        {
            var Goods = await _GoodsRepo.GetAll(query);
            return Ok(Goods);
        }
        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetByid(int id)
        {
            return Ok(await _GoodsRepo.GetById(id));
        }
        [HttpPost("Add")]
        public async Task<IActionResult> Add(GoodsVM Good)
        {
            return Ok(await _GoodsRepo.Add(Good));
        }
        [HttpPut("Edit")]
        public async Task<IActionResult> Edit(GoodsMD Good)
        {
            return Ok(await _GoodsRepo.Edit(Good));
        }
        [HttpDelete("Delete")]
          public async Task<IActionResult> Delete(int id)
        {
            return Ok(await _GoodsRepo.Delete(id));
        }
        [HttpPut("AddPic")]
        public async Task<IActionResult> AddPic([FromForm]Goodpic idpic, List<IFormFile> files)
        {
            return Ok(await _GoodsRepo.AddPic(idpic,files));
        }

        [HttpGet("getbycate/{id}")]
        public async Task<IActionResult> GetByCate(int id)
        {
            return Ok(await _GoodsRepo.GetByCate(id));
        }
    }
}
