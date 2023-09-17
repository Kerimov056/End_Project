using AutoMapper;
using EndProject.Application.DTOs.Wishlist;
using EndProject.Domain.Entitys;

namespace EndProjet.Persistance.MapperProfiles;

public class WishlistProfile : Profile
{
    public WishlistProfile()
    {
        CreateMap<WishlistProduct, WishlistProductDto>().ReverseMap();
    }
}
