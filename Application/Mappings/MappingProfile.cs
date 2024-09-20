using Application.DTOs;
using AutoMapper;
using Domain.Entities;

namespace Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Маппинг между сущностью Book и BookDto
            CreateMap<Book, BookDto>()
                .ForMember(dest => dest.AuthorName, opt => opt.MapFrom(src => src.Author.FirstName + " " + src.Author.LastName))
                .ReverseMap(); // Добавляем обратный маппинг

            // Маппинг между сущностью Author и AuthorDto
            CreateMap<Author, AuthorDto>().ReverseMap();
        }
    }
}