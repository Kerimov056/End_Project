using AutoMapper;
using EndProject.Application.DTOs.ShareTrip;
using EndProject.Domain.Entitys;

namespace EndProjet.Persistance.MapperProfiles;

public class ShareTripProfile : Profile
{
    public ShareTripProfile()
    {
        CreateMap<ShareTrip, ShareTripCreateDTO>().ReverseMap();
        CreateMap<ShareTrip, ShareTripGetDTO>().ReverseMap();
        CreateMap<ShareTrip, ShareTripUpdateDTO>().ReverseMap();
        CreateMap<ShareTripCreateDTO, ShareTripUpdateDTO>().ReverseMap();
    }
}
