using AutoMapper;
using EndProject.Application.DTOs.Trip;
using EndProject.Domain.Entitys;

namespace EndProjet.Persistance.MapperProfiles;

public class TripProfile : Profile
{
    public TripProfile()
    {
        CreateMap<Trip, TripCreateDTO>().ReverseMap();
        CreateMap<Trip, TripGetDTO>().ReverseMap();
        CreateMap<Trip, TripUpdateDTO>().ReverseMap();
    }
}
