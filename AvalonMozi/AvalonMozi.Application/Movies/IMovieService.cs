using AvalonMozi.Domain.Movies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvalonMozi.Application.Movies
{
    public interface IMovieService
    {
        Task<List<Movie>> GetMovies();
        Task<bool> AddNewMovie(Movie movie);
    }
}
