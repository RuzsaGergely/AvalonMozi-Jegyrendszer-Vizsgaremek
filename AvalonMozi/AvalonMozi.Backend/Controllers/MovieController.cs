using AvalonMozi.Application.Movies;
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

        [HttpGet]
        [AllowAnonymous]
        public async Task<List<MovieDto>> GetMovies()
        {
            return _movieFactory.ConvertEntityListToDtoList(await _movieService.GetMovies());
        }

        [HttpPost("AddMovie")]
        public async Task AddMovie(MovieDto movie)
        {
            var newEntity = _movieFactory.ConvertDtoToEntity(movie);
            try
            {
                await _movieService.AddNewMovie(newEntity);
            }
            catch (Exception)
            {
            }
        }
    } 
}
