using AutoMapper;
using EndProject.Application.DTOs.Car;
using EndProject.Domain.Entitys;

namespace EndProjet.Persistance.MapperProfiles;

public class CarProfile:Profile
{
    public CarProfile()
    {
        CreateMap<Car, CarGetDTO>()
               .ForMember(dest => dest.CarImages, opt => opt.MapFrom(src => src.carImages.Select(ci => ci.imagePath)))
               .ForMember(dest => dest.CarTags, opt => opt.MapFrom(src => src.carTags.Select(ct => ct.Tag.tag)));

        CreateMap<CarCreateDTO, Car>().ReverseMap();
    }
}
