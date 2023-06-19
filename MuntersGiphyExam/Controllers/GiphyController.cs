using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MuntersGiphyExam.Services.Interfaces;

namespace MuntersGiphyExam.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GiphyController : ControllerBase
    {
        private readonly IGiphyService _giphyService;

        public GiphyController(IGiphyService giphyService)
        {
            _giphyService = giphyService;
        }

        [HttpGet("search/{query}")]
        public async Task<IActionResult> SearchGifs(string query)
        {
            try
            {
                string result = await _giphyService.SearchGifs(query);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpGet("trending")]
        public async Task<IActionResult> GetTrendingGifs()
        {
            try
            {
                string result = await _giphyService.GetTrendingGifs();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
    }
}
