using AutoMapper;
using EndProject.Application.DTOs.Communication;
using EndProject.Domain.Entitys;

namespace EndProjet.Persistance.MapperProfiles;

public class CommunicationProfile : Profile
{
    public CommunicationProfile()
    {
        CreateMap<Communication, CommunicationGetDTO>().ReverseMap();
        CreateMap<Communication, CommunicationCreateDTO>().ReverseMap();
    }
}
