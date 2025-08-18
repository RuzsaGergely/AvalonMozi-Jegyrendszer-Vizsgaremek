using AvalonMozi.Domain.Movies;
using AvalonMozi.Factories.MovieFactories;
using AvalonMozi.Factories.MovieFactories.Dto;
using AvalonMozi.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvalonMozi.Application.Movies.Services
{
    public class MovieService : IMovieService
    {
        private readonly AvalonContext _context;
        private readonly IMovieFactory _movieFactory;
        public MovieService(AvalonContext context, IMovieFactory movieFactory)
        {
            _context = context;
            _movieFactory = movieFactory;
        }
        public async Task<List<Movie>> GetMovies()
        {
            return await _context.Movies.Where(x => !x.Deleted).ToListAsync();
        }

        public async Task<bool> AddNewMovie(Movie movie)
        {
            await _context.Movies.AddAsync(movie);
            var created = await _context.SaveChangesAsync();

            return created > 0;
        }

        public async Task<bool> UpdateMovie(MovieDto dto)
        {
            var movie = await _context.Movies.Where(x=>x.TechnicalId == dto.TechnicalId).FirstOrDefaultAsync();

            movie.Title = dto.Title;
            movie.SeoFriendlyTitle = dto.SeoFriendlyTitle;
            movie.TicketPrice = dto.TicketPrice;
            movie.AgeRestriction = dto.AgeRestriction;
            movie.Description = dto.Description;

            foreach (var item in movie.Dates.Where(x=>!x.Deleted))
            {
                if(!dto.Dates.Any(x=>x.DateFrom == item.DateFrom && x.DateTo == item.DateTo))
                {
                    item.Deleted = true;
                }
            }

            foreach (var item in dto.Dates)
            {
                if(!movie.Dates.Any(x=>x.DateFrom == item.DateFrom && x.DateTo == item.DateTo))
                {
                    movie.Dates.Add(_movieFactory.ConvertMovieDateDtoToEntity(item));
                }
            }

            _context.Movies.Update(movie);
            var updated = await _context.SaveChangesAsync();

            return updated > 0;
        }

        public async Task DeleteMovie(string technicalId)
        {
            var movie = await _context.Movies.Where(x => x.TechnicalId == technicalId).FirstOrDefaultAsync();
            if(movie is not null)
            {
                movie.Deleted = true;
                foreach (var item in movie.Dates)
                {
                    item.Deleted = true;
                }
                await _context.SaveChangesAsync();
            }
        }

        public async Task<Movie> GetMovieByTechnicalId(string technicalid)
        {
            return await _context.Movies.Where(x => x.TechnicalId == technicalid).FirstOrDefaultAsync();
        }

        public async Task<Movie> GetMovieBySeoTitle(string seotitle)
        {
            return await _context.Movies.Where(x => x.SeoFriendlyTitle == seotitle).FirstOrDefaultAsync();
        }
    }
}
