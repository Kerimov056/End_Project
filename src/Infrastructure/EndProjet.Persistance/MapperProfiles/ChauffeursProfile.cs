using AutoMapper;
using EndProject.Application.DTOs.Chauffeurs;
using EndProject.Domain.Entitys;

namespace EndProjet.Persistance.MapperProfiles;

public class ChauffeursProfile:Profile
{
    public ChauffeursProfile()
    {
        CreateMap<Chauffeurs, ChauffeursCreateDTO>().ReverseMap();
        CreateMap<Chauffeurs, ChauffeursGetDTO>().ReverseMap();
        CreateMap<Chauffeurs, ChauffeursUpdateDTO>().ReverseMap();
    }
}
