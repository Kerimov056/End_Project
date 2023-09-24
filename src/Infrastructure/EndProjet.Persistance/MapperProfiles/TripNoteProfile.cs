using AutoMapper;
using EndProject.Application.DTOs.Trip;
using EndProject.Domain.Entitys;

namespace EndProjet.Persistance.MapperProfiles;

public class TripNoteProfile : Profile
{
    public TripNoteProfile()
    {
        CreateMap<TripNote, TripCreateDTO>().ReverseMap();
        CreateMap<TripNote, TripGetDTO>().ReverseMap();
        CreateMap<TripNote, TripUpdateDTO>().ReverseMap();
    }
}
