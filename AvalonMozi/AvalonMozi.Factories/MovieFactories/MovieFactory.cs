using AvalonMozi.Domain.Movies;
using AvalonMozi.Domain.Users;
using AvalonMozi.Factories.MovieFactories.Dto;
using AvalonMozi.Factories.UserFactories.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvalonMozi.Factories.MovieFactories
{
    public interface IMovieFactory
    {
        Movie ConvertDtoToEntity(MovieDto dto);
        MovieDto ConvertEntityToDto(Movie entity);
        List<MovieDto> ConvertEntityListToDtoList(List<Movie> list);
        MovieDate ConvertMovieDateDtoToEntity(MovieDateDto date);
        MovieDateDto ConvertMovieDateEntityToDto(MovieDate date);
    }

    public class MovieFactory : IMovieFactory
    {
        public Movie ConvertDtoToEntity(MovieDto dto)
        {
            var movie = new Movie()
            {
                AgeRestriction = dto.AgeRestriction,
                Deleted = false,
                Description = dto.Description,
                SeoFriendlyTitle = dto.SeoFriendlyTitle,
                TechnicalId = Guid.NewGuid().ToString(),
                TicketPrice = dto.TicketPrice,
                Title = dto.Title,
                Dates = new List<MovieDate>(),
                CoverImageBase64 = dto.CoverImageBase64
            };

            foreach (var item in dto.Dates)
            {
                movie.Dates.Add(this.ConvertMovieDateDtoToEntity(item));
            }
            return movie;
        }

        public MovieDto ConvertEntityToDto(Movie entity)
        {
            var movie = new MovieDto()
            {
                AgeRestriction = entity.AgeRestriction,
                Description = entity.Description,
                SeoFriendlyTitle = entity.SeoFriendlyTitle,
                TechnicalId = entity.TechnicalId,
                TicketPrice = entity.TicketPrice,
                Title = entity.Title,
                Dates = new List<MovieDateDto>(),
                CoverImageBase64 = entity.CoverImageBase64
            };

            foreach (var item in entity.Dates)
            {
                movie.Dates.Add(this.ConvertMovieDateEntityToDto(item));
            }

            return movie;
        }

        public List<MovieDto> ConvertEntityListToDtoList(List<Movie> list)
        {
            var returnList = new List<MovieDto>();
            foreach (var movie in list)
            {
                returnList.Add(this.ConvertEntityToDto(movie));
            }
            return returnList;
        }

        public MovieDateDto ConvertMovieDateEntityToDto(MovieDate date)
        {
            return new MovieDateDto()
            {
                TechnicalId = date.TechnicalId,
                DateFrom = date.DateFrom,
                DateTo = date.DateTo
            };
        }

        public MovieDate ConvertMovieDateDtoToEntity(MovieDateDto date)
        {
            return new MovieDate()
            {
                DateFrom = date.DateFrom,
                DateTo = date.DateTo,
                Deleted = false,
                TechnicalId = Guid.NewGuid().ToString()
            };
        }
    }
}
