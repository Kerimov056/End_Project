using AutoMapper;
using EndProject.Application.Abstraction.Repositories.IEntityRepository;
using EndProject.Application.Abstraction.Services;
using EndProject.Application.DTOs.Wishlist;
using EndProject.Domain.Entitys;
using EndProjet.Persistance.Migrations;
using Microsoft.EntityFrameworkCore;

namespace EndProjet.Persistance.Implementations.Services;

public class WishlistServices : IWishlistServices
{
    private readonly IWishlistReadRepository _wishlistReadRepository;
    private readonly IWishlistWriteRepository _wishlistWriteRepository;
    private readonly IMapper _mapper;
    private readonly IWishlistProductServices _wishlistProductServices;
    private readonly ICarServices _carServices;

    public WishlistServices(IWishlistReadRepository wishlistReadRepository,
                            IWishlistWriteRepository wishlistWriteRepository,
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

    public async Task AddWishlistAsync(Guid Id, string AppUserId)
    {
        var wishlist = await _wishlistReadRepository
                            .Table
                            .Include(x => x.WishlistProducts)
                            .FirstOrDefaultAsync(x => x.AppUserId == AppUserId);
        if (wishlist is null)
        {
            wishlist = new Wishlist
            {
                AppUserId = AppUserId,
            };
            await _wishlistWriteRepository.AddAsync(wishlist);
            await _wishlistWriteRepository.SavaChangeAsync();
        }

        var wishlistProduct = wishlist.WishlistProducts.FirstOrDefault(x => x.CarId == Id && x.WishlistId == wishlist.Id);
        if (wishlistProduct is not null) wishlistProduct.Quantity = 1;
        else
        {
            wishlistProduct = new EndProject.Domain.Entitys.WishlistProduct
            {
                WishlistId = wishlist.Id,
                CarId = Id,
                Quantity = 1
            };
            wishlist.WishlistProducts.Add(wishlistProduct);
        }
        await _wishlistWriteRepository.SavaChangeAsync();
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
