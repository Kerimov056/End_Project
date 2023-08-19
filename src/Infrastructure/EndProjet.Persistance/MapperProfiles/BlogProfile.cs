using AutoMapper;
using EndProject.Application.DTOs.Blog;
using EndProject.Domain.Entitys;

namespace EndProjet.Persistance.MapperProfiles;

public class BlogProfile : Profile
{
    public BlogProfile()
    {
        CreateMap<Blog, BlogGetDTO>().ReverseMap();
        CreateMap<Blog, BlogCreateDTO>().ReverseMap();
        CreateMap<Blog, BlogUpdateDTO>().ReverseMap();
    }
}
