using AutoMapper;
using EndProject.Application.DTOs.CarImage;
using EndProject.Domain.Entitys;

namespace EndProjet.Persistance.MapperProfiles;

public class CarImageProfile:Profile
{
    public CarImageProfile()
    {
        CreateMap<CarImage, CarImageGetDTO>().ReverseMap();
        CreateMap<CarImage, CarImageCreateDTO>().ReverseMap();
        CreateMap<CarImage, CarImageUpdateDTO>().ReverseMap();
    }
}
