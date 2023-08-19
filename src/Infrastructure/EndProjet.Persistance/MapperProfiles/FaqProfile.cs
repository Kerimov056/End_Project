using AutoMapper;
using EndProject.Application.DTOs.Faq;
using EndProject.Domain.Entitys;

namespace EndProjet.Persistance.MapperProfiles;

public class FaqProfile:Profile
{
    public FaqProfile()
    {
        CreateMap<Faq, FaqGetDTO>().ReverseMap();
        CreateMap<Faq, FaqCreateDTO>().ReverseMap();
        CreateMap<Faq, FaqUpdateDTO>().ReverseMap();
    }
}
