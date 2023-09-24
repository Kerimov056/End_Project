using AutoMapper;
using EndProject.Application.DTOs.Trip;
using EndProject.Application.DTOs.TripNote;
using EndProject.Domain.Entitys;

namespace EndProjet.Persistance.MapperProfiles;

public class TripNoteProfile : Profile
{
    public TripNoteProfile()
    {
        CreateMap<TripNote, TripNoteCreateDTO>().ReverseMap();
        CreateMap<TripNote, TripNoteGetDTO>().ReverseMap();
        CreateMap<TripNote, TripNoteUpdateDTO>().ReverseMap();
    }
}
