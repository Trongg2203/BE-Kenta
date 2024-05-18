using KenTaShop.Services;
using KenTaShop.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KenTaShop.Controllers
{
    [Authorize(Roles = "admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class ProducerController : ControllerBase
    {
        private readonly IProducerRepository _ProducerRepo;
        public ProducerController(IProducerRepository producerRepository)
        {
            _ProducerRepo = producerRepository;
        }
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var producer = await _ProducerRepo.GetAll();
            return Ok(producer);
        }
        [HttpPost("AddProducer")]
        public async Task<IActionResult> AddProducer(ProducerVM producerVM)
        {
            var producer = await _ProducerRepo.AddProducer(producerVM);
            return Ok(producer);
        }
        [HttpPut("EditProducer")]
        public async Task<IActionResult> EditProducer(int idProducer, ProducerVM producerVM)
        {
            var producer = await _ProducerRepo.EditProducer(idProducer, producerVM);
            return Ok(producer);
        }
        [HttpDelete("DeleteProducer")]
        public async Task<IActionResult> DeleteProducer(int idProducer)
        {
            var producer = await _ProducerRepo.DeleteProducer(idProducer);
            return Ok(producer);
        }
        [HttpGet("getbyid")]
        public async Task<ActionResult<ProducerMD>> GetById(int id)
        {
            if (id <= 0)
            {
                return BadRequest("Id không hợp lệ");
            }
            var producer = await _ProducerRepo.GetById(id);
            if (producer is null) { return NotFound("không tìm thấy"); }
            return Ok(producer);
        }
    }
}
