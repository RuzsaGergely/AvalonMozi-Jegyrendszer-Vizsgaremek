using AvalonMozi.Domain.Movies;
using AvalonMozi.Factories.MovieFactories.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvalonMozi.Application.Movies.Services
{
    public interface IMovieService
    {
        Task<List<Movie>> GetMovies();
        Task<bool> AddNewMovie(Movie movie);
        Task<bool> UpdateMovie(MovieDto dto);
        Task DeleteMovie(string technicalId);
    }
}
