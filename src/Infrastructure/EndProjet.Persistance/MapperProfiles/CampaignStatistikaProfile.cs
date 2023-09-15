using AutoMapper;
using EndProject.Application.DTOs.CampaignStatistika;
using EndProject.Domain.Entitys;

namespace EndProjet.Persistance.MapperProfiles;

public class CampaignStatistikaProfile : Profile
{
    public CampaignStatistikaProfile()
    {
        CreateMap<CampaignStatistika, CampaignStatistikaGetDTO>().ReverseMap();
        CreateMap<CampaignStatistika, CampaignStatistikaCreateDTO>().ReverseMap();
    }
}
