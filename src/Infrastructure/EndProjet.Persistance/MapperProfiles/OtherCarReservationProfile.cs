using AutoMapper;
using EndProject.Application.DTOs.OtherCarReservation;
using EndProject.Domain.Entitys;

namespace EndProjet.Persistance.MapperProfiles;

public class OtherCarReservationProfile:Profile
{
    public OtherCarReservationProfile()
    {
        CreateMap<OtherCarReservation, OtherCarReservationCreateDTO>().ReverseMap();
        CreateMap<OtherCarReservation, OtherCarReservationGetDTO>().ReverseMap();
        CreateMap<OtherCarReservation, OtherCarReservationUpdateDTO>().ReverseMap();
    }
}
