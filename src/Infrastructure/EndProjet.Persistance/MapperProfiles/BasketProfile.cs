using AutoMapper;
using EndProject.Application.DTOs.Basket;
using EndProject.Domain.Entitys;

namespace EndProjet.Persistance.MapperProfiles;

public class BasketProfile : Profile
{
    public BasketProfile()
    {
        CreateMap<BasketProduct, BasketProductListDto>().ReverseMap();
    }
}
