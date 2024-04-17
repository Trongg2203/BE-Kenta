using KenTaShop.Services;
using KenTaShop.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;

namespace KenTaShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RatingController : ControllerBase
    {
        private readonly IRatingRepository _ratingRepo;

        public RatingController(IRatingRepository ratingRepo)
        {
            _ratingRepo = ratingRepo;
        }
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var rating = await _ratingRepo.GetAll();
            return Ok(rating);
        }
        [HttpGet("GetByIdBillAndIdGood")]
        public async Task<IActionResult> Get(int idbill, int idgood)
        {
            var rating = await _ratingRepo.GetById(idbill, idgood);
            return Ok(rating);
        }
        [HttpPost("Add")]
        public async Task<IActionResult> Add(RatingVM Rating)
        {
            var rating = await _ratingRepo.Add(Rating);
            return Ok(rating);
        }
        [HttpPut("Edit")]
        public async Task<IActionResult> Edit(RatingVM Rating)
        {
            var rating = await _ratingRepo.Edit(Rating);
            return Ok(rating);
        }
        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete(RatingVM Rating)
        {
            var rating = await _ratingRepo.Delete(Rating);
            return Ok(rating);
        }
    }
}
