using AutoMapper;
using EndProject.Application.DTOs.CarReservation;
using EndProject.Domain.Entitys;

namespace EndProjet.Persistance.MapperProfiles;

public class CarReservationProfile:Profile
{
    public CarReservationProfile()
    {
        CreateMap<CarReservation, CarReservationCreateDTO>().ReverseMap();
        CreateMap<CarReservation, CarReservationGetDTO>().ReverseMap();
        CreateMap<CarReservation, CarReservationUpdateDTO>().ReverseMap();
    }
}
