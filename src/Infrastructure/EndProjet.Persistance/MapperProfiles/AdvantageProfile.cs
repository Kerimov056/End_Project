using AutoMapper;
using EndProject.Application.DTOs.Advantage;
using EndProject.Domain.Entitys;

namespace EndProjet.Persistance.MapperProfiles;

public class AdvantageProfile:Profile
{
    public AdvantageProfile()
    {
        CreateMap<Advantage, AdvantageGetDTO>().ReverseMap();
        CreateMap<Advantage, AdvantageCreateDTO>().ReverseMap();
        CreateMap<Advantage, AdvantageUpdateDTO>().ReverseMap();
    }
}
