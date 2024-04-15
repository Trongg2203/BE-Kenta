using KenTaShop.Services;
using KenTaShop.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KenTaShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GoodstypeController : ControllerBase
    {
        private readonly IGoodstypeRepository _goodstypeRepository;
        public GoodstypeController(IGoodstypeRepository goodstypeRepository)
        {
            _goodstypeRepository = goodstypeRepository;
        }
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var goodstype = await _goodstypeRepository.GetAll();
            return Ok(goodstype);
        }
        [HttpPost("AddGoodstype")]
        public async Task<IActionResult> AddGoodstype(GoodstypeVM goodstypeVM)
        {
            var goodstype = await _goodstypeRepository.AddGoodstype(goodstypeVM);
            return Ok(goodstype);
        }
        [HttpPut("EditGoodstype")]
        public async Task<IActionResult> EditGoodstype(int idgoodstype, GoodstypeVM goodstypeVM)
        {
            var goodstype = await _goodstypeRepository.EditGoodstype(idgoodstype, goodstypeVM);
            return Ok(goodstype);
        }
        [HttpDelete("DeleteGoodstype")]
        public async Task<IActionResult> DeleteGoodstype(int idgoodstype)
        {
            var goodstype = await _goodstypeRepository.DeleteGoodstype(idgoodstype);
            return Ok(goodstype);
        }
        [HttpGet("getbyid")]
        public async Task<ActionResult<GoodstypeMD>> GetById(int id)
        {
            if (id <= 0)
            {
                return BadRequest("Id không hợp lệ");
            }
            var goodstype = await _goodstypeRepository.GetById(id);
            if (goodstype is null) { return NotFound("không tìm thấy"); }
            return Ok(goodstype);
        }
    }
}
