using ElasticsearchCrud.Repository;
using Microsoft.AspNetCore.Mvc;

namespace ElasticsearchCrud.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MoviesController : ControllerBase
    {
        private readonly IElasticRepository _elasticRepository;

        public MoviesController(IElasticRepository elasticRepository)
        {
            _elasticRepository = elasticRepository;
        }
        [HttpGet("")]
        public async Task<IActionResult> GetAllMovies()
        {
            var result = await _elasticRepository.GetAllMovies();

            return Ok(result);
        }

        [HttpGet("title-prefix")]
        public async Task<IActionResult> GetAllMoviesByTitleField([FromQuery] string titleField)
        {
            var result = await _elasticRepository.GetAllMoviesByTitleField(titleField);

            return Ok(result);
        }

        [HttpGet("id")]
        public async Task<IActionResult> GetMovieById([FromQuery] string id)
        {
            var result = await _elasticRepository.GetMovieById(id);
            return Ok(result);
        }
    }
}