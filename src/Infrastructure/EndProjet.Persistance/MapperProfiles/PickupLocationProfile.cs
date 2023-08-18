using AutoMapper;
using EndProject.Application.DTOs.PickupLocation;
using EndProject.Domain.Entitys;

namespace EndProjet.Persistance.MapperProfiles;

public class PickupLocationProfile:Profile
{
    public PickupLocationProfile()
    {
        CreateMap<PickupLocation, PickupLocationGetDTO>().ReverseMap();
        CreateMap<PickupLocation, PickupLocationDTO>().ReverseMap();
    }
}
