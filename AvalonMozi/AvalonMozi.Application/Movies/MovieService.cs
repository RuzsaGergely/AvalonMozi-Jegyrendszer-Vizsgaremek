using AvalonMozi.Domain.Movies;
using AvalonMozi.Factories.MovieFactories.Dto;
using AvalonMozi.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvalonMozi.Application.Movies
{
    public class MovieService : IMovieService
    {
        private readonly AvalonContext _context;
        public MovieService(AvalonContext context)
        {
            _context = context;
        }
        public async Task<List<Movie>> GetMovies()
        {
            return await _context.Movies.Include(x => x.Dates).Where(x => !x.Deleted).ToListAsync();
        }

        public async Task<bool> AddNewMovie(Movie movie)
        {
            await _context.Movies.AddAsync(movie);
            var created = await _context.SaveChangesAsync();

            return created > 0;
        }
    }
}
