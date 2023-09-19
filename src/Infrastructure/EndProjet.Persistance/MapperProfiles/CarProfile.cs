using AutoMapper;
using EndProject.Application.DTOs.Car;
using EndProject.Domain.Entitys;

namespace EndProjet.Persistance.MapperProfiles;

public class CarProfile:Profile
{
    public CarProfile()
    {
        CreateMap<Car, CarGetDTO>().ReverseMap();
        CreateMap<Car, GameCarGetDTO>().ReverseMap();
        CreateMap<CarCreateDTO, Car>().ReverseMap();
    }
}
