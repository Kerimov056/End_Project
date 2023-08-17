using AutoMapper;
using EndProject.Application.DTOs.Category;
using EndProject.Domain.Entitys;

namespace EndProjet.Persistance.MapperProfiles;

public class CarCategoryProfile:Profile
{
    public CarCategoryProfile()
    {
        CreateMap<CarCategory, CarCategoryGetDTO>().ReverseMap();
        CreateMap<CarCategory, CarCategoryCreateDTO>().ReverseMap();
        CreateMap<CarCategory, CarCategoryUpdateDTO>().ReverseMap();
    }
}
