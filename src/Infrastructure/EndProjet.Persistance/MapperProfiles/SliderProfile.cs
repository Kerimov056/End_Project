using AutoMapper;
using EndProject.Application.DTOs.Slider;
using EndProject.Domain.Entitys;

namespace EndProjet.Persistance.MapperProfiles;

public class SliderProfile:Profile
{
    public SliderProfile()
    {
        CreateMap<Slider, SliderGetDTO>().ReverseMap();
        CreateMap<Slider, SliderCreateDTO>().ReverseMap();
        CreateMap<Slider, SliderUpdateDTO>().ReverseMap();
    }
}
