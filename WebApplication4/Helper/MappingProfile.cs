using AutoMapper;
using WebApplication4.DTO;

namespace WebApplication4.Helper
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<Movie, MovieDetailsDto>();

            CreateMap<MovieDto, Movie>()
                .ForMember(src => src.Poster, opt => opt.Ignore());

            CreateMap<MovieDto, MovieDetailsDto>()
                .ForMember(src => src.Poster, opt => opt.Ignore());

        }
    }
}
