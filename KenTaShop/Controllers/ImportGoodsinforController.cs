using KenTaShop.Services;
using KenTaShop.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KenTaShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImportGoodsinforController : ControllerBase
    {
        private readonly IImportGoodsinforRepository _importGoodsinforRepo;
        public ImportGoodsinforController(IImportGoodsinforRepository importGoodsinforRepo)
        {
            _importGoodsinforRepo = importGoodsinforRepo;
        }
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var importgoodsinfor = await _importGoodsinforRepo.GetAll();
            return Ok(importgoodsinfor);
        }
        [HttpPost("AddImportGoodsinfor")]
        public async Task<IActionResult> AddImportGoodsinfor(ImportGoodsinforVM importGoodsinforVM)
        {
            var importgoodsinfor = await _importGoodsinforRepo.AddImportGoodsinfor(importGoodsinforVM);
            return Ok(importgoodsinfor);
        }
        [HttpPut("EditImportGoodsinfor")]
        public async Task<IActionResult> EditImportGoodsinfor(int idimportGoodsinfor, ImportGoodsinforVM importGoodsinforVM)
        {
            var importgoodsinfor = await _importGoodsinforRepo.EditImportGoodsinfor(idimportGoodsinfor, importGoodsinforVM);
            return Ok(importgoodsinfor);
        }
        [HttpDelete("DeleteImportGoodsinfor")]
        public async Task<IActionResult> DeleteImportGoodsinfor(int idimportGoodsinfor)
        {
            var importgoodsinfor = await _importGoodsinforRepo.DeleteImportGoodsinfor(idimportGoodsinfor);
            return Ok(importgoodsinfor);
        }
        [HttpGet("getbyid")]
        public async Task<ActionResult<ImportGoodsinforMD>> GetById(int id)
        {
            if (id <= 0)
            {
                return BadRequest("Id không hợp lệ");
            }
            var importgoodsinfor = await _importGoodsinforRepo.GetById(id);
            if (importgoodsinfor is null) { return NotFound("không tìm thấy"); }
            return Ok(importgoodsinfor);
        }
    }
}
