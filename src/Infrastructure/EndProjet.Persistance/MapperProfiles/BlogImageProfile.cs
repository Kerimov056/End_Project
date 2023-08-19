using AutoMapper;
using EndProject.Application.DTOs.BlogImage;
using EndProject.Domain.Entitys;

namespace EndProjet.Persistance.MapperProfiles;

public class BlogImageProfile : Profile
{
    public BlogImageProfile()
    {
        CreateMap<BlogImage, BlogImageGetDTO>().ReverseMap();
        CreateMap<BlogImage, BlogImageCreateDTO>().ReverseMap();
        CreateMap<BlogImage, BlogImageUpdateDTO>().ReverseMap();
    }
}
