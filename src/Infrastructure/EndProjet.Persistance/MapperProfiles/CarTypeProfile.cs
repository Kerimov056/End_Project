using AutoMapper;
using EndProject.Application.DTOs.CarType;
using EndProject.Domain.Entitys;

namespace EndProjet.Persistance.MapperProfiles;

public class CarTypeProfile:Profile
{
    public CarTypeProfile()
    {
        CreateMap<CarTypeGetDTO, CarType>().ReverseMap();
        CreateMap<CarTypeCreateDTO, CarType>().ReverseMap();
        CreateMap<CarTypeUpdateDTO, CarType>().ReverseMap();
    }
}
