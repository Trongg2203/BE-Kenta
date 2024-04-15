using KenTaShop.Services;
using KenTaShop.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KenTaShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImportGoodsController : ControllerBase
    {
        private readonly IImportGoodRepository _importGoodRepo;
        public ImportGoodsController(IImportGoodRepository importGoodRepo)
        {
            _importGoodRepo = importGoodRepo;
        }
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var importgood = await _importGoodRepo.GetAll();
            return Ok(importgood);
        }
        [HttpPost("AddImportGood")]
        public async Task<IActionResult> AddImportGood(ImportGoodVM importGoodVM)
        {
            var importgood = await _importGoodRepo.AddImportGood(importGoodVM);
            return Ok(importgood);
        }
        [HttpPut("EditImportGood")]
        public async Task<IActionResult> EditImportGood(int idimportGood, ImportGoodVM importGoodVM)
        {
            var importgood = await _importGoodRepo.EditImportGood(idimportGood, importGoodVM);
            return Ok(importgood);
        }
        [HttpDelete("DeleteImportGood")]
        public async Task<IActionResult> DeleteImportGood(int idimportGood)
        {
            var importgood = await _importGoodRepo.DeleteImportGood(idimportGood);
            return Ok(importgood);
        }
        [HttpGet("getbyid")]
        public async Task<ActionResult<ImportGoodMD>> GetById(int id)
        {
            if (id <= 0)
            {
                return BadRequest("Id không hợp lệ");
            }
            var importgood = await _importGoodRepo.GetById(id);
            if (importgood is null) { return NotFound("không tìm thấy"); }
            return Ok(importgood);
        }
    }
}
