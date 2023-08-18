using AutoMapper;
using EndProject.Application.DTOs.ReturnLocation;
using EndProject.Domain.Entitys;

namespace EndProjet.Persistance.MapperProfiles;

public class ReturnLocationProfile:Profile
{
    public ReturnLocationProfile()
    {
        CreateMap<ReturnLocation, ReturnLocationGetDTO>().ReverseMap();
        CreateMap<ReturnLocation, ReturnLocationDTO>().ReverseMap();
    }
}
