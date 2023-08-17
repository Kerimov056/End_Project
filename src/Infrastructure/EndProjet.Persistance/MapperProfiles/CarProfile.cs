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


        CreateMap<CarCreateDTO, Car>()
                 .ForMember(dest => dest.Marka, opt => opt.MapFrom(src => src.Marka))
                 .ForMember(dest => dest.Model, opt => opt.MapFrom(src => src.Model))
                 .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price))
                 .ForMember(dest => dest.Year, opt => opt.MapFrom(src => src.Year))
                 .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                 .ForMember(dest => dest.carType, opt => opt.Ignore()) 
                 .ForMember(dest => dest.carCategory, opt => opt.Ignore())
                 .ForMember(dest => dest.carImages, opt => opt.Ignore()) 
                 .ForMember(dest => dest.carTags, opt => opt.Ignore()) 
                 .ForMember(dest => dest.Reservations, opt => opt.Ignore()); 
    }
}
