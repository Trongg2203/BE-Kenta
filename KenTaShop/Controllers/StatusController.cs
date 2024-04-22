using KenTaShop.Services;
using KenTaShop.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KenTaShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatusController : ControllerBase
    {
        private readonly IStatusRepository _StatusRepo;

        public StatusController(IStatusRepository StatusRepo)
        {
            _StatusRepo = StatusRepo;
        }
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var status = await _StatusRepo.GetAll();
            return Ok(status);
        }
        [HttpGet("GetById")]
        public async Task<IActionResult> GetById(int id)
        {
            var status = await _StatusRepo.GetById(id);
            return Ok(status);
        }
        [HttpPost("Add")]
        public async Task<IActionResult> Add(string sta)
        {
            var status = await _StatusRepo.Add(sta);
            return Ok(status);
        }
        [HttpPut("Edit")]
        public async Task<IActionResult> Edit(StatusVM sta)
        {
            var status = await _StatusRepo.Edit(sta);
            return Ok(status);
        }
        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete(int id)
        {
            var status = await _StatusRepo.Delete(id);
            return Ok(status);
        }
    }
}
