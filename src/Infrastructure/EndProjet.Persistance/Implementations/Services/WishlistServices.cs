using AutoMapper;
using EndProject.Application.Abstraction.Repositories.IEntityRepository;
using EndProject.Application.Abstraction.Services;
using EndProject.Application.DTOs.Wishlist;

namespace EndProjet.Persistance.Implementations.Services;

public class WishlistServices : IWishlistServices
{
    private readonly IWishlistProductReadRepository _wishlistReadRepository;
    private readonly IWishlistProductWriteRepository _wishlistWriteRepository;
    private readonly IMapper _mapper;
    private readonly IWishlistProductServices _wishlistProductServices;
    private readonly ICarServices _carServices;

    public WishlistServices(IWishlistProductReadRepository wishlistReadRepository,
                            IWishlistProductWriteRepository wishlistWriteRepository,
                            IMapper mapper,
                            IWishlistProductServices wishlistProductServices,
                            ICarServices carServices)
    {
        _wishlistReadRepository = wishlistReadRepository;
        _wishlistWriteRepository = wishlistWriteRepository;
        _mapper = mapper;
        _wishlistProductServices = wishlistProductServices;
        _carServices = carServices;
    }

    public Task AddWishlistAsync(Guid Id, string AppUserId)
    {
        throw new NotImplementedException();
    }

    public Task DeleteBasketAsync(Guid id, string AppUserId)
    {
        throw new NotImplementedException();
    }

    public Task DeleteWishlistItemAsync(Guid carId, string AppUserId)
    {
        throw new NotImplementedException();
    }

    public Task<int> GetWishlistCountAsync(string AppUserId)
    {
        throw new NotImplementedException();
    }

    public Task<List<WishlistProductDto>> GetWishlistProductsAsync(string AppUserId)
    {
        throw new NotImplementedException();
    }
}
