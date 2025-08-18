using AvalonMozi.Application.Movies.Services;
using AvalonMozi.Factories.MovieFactories;
using AvalonMozi.Factories.MovieFactories.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AvalonMozi.Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "ADMIN,EMPLOYEE")]
    public class MovieController : ControllerBase
    {
        private readonly IMovieService _movieService;
        private readonly IMovieFactory _movieFactory;
        public MovieController(IMovieService movieService, IMovieFactory movieFactory)
        {
            _movieService = movieService;
            _movieFactory = movieFactory;
        }

        [HttpGet("GetMovies")]
        [AllowAnonymous]
        public async Task<List<MovieDto>> GetMovies()
        {
            return _movieFactory.ConvertEntityListToDtoList(await _movieService.GetMovies());
        }

        [HttpGet("GetMovieByTechnicalId")]
        [AllowAnonymous]
        public async Task<MovieDto> GetMovieByTechnicalId(string technicalId)
        {
            return _movieFactory.ConvertEntityToDto(await _movieService.GetMovieByTechnicalId(technicalId));
        }

        [HttpGet("GetMovieBySeoTitle")]
        [AllowAnonymous]
        public async Task<MovieDto> GetMovieBySeoTitle(string seotitle)
        {
            return _movieFactory.ConvertEntityToDto(await _movieService.GetMovieBySeoTitle(seotitle));
        }

        [HttpPost("AddMovie")]
        public async Task<IActionResult> AddMovie(MovieDto movie)
        {
            var newEntity = _movieFactory.ConvertDtoToEntity(movie);
            try
            {
                if(await _movieService.AddNewMovie(newEntity))
                {
                    return Ok();
                }
                return BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPut("UpdateMovie")]
        public async Task<IActionResult> UpdateMovie(MovieDto dto)
        {
            try
            {
                if(await _movieService.UpdateMovie(dto))
                {
                    return Ok();
                }
                return BadRequest();
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("DeleteMovie")]
        public async Task DeleteMovie(string techId)
        {
            await _movieService.DeleteMovie(techId);
        }
    } 
}
