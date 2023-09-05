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


//using AutoMapper;
//using EndProject.Application.DTOs.Slider;
//using EndProject.Domain.Entitys;
//using Microsoft.AspNetCore.Http;

//namespace EndProjet.Persistance.MapperProfiles;

//public class SliderProfile : Profile
//{
//    private readonly IHttpContextAccessor _httpAccessor;

//    public SliderProfile(IHttpContextAccessor httpAccessor)
//    {
//        _httpAccessor = httpAccessor;

//        CreateMap<Slider, SliderGetDTO>().ForMember(x => x.imagePath, y => y.MapFrom(a => $"{_httpAccessor.HttpContext.Request.Scheme}://{_httpAccessor.HttpContext.Request.Host}/sliders/{a.Imagepath}")).ReverseMap();
//        CreateMap<Slider, SliderCreateDTO>().ReverseMap();
//        CreateMap<Slider, SliderUpdateDTO>().ReverseMap();
//    }
//}



