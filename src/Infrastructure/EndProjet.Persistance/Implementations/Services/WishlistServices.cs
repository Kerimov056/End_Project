using AutoMapper;
using EndProject.Application.Abstraction.Repositories.IEntityRepository;
using EndProject.Application.Abstraction.Services;
using EndProject.Application.DTOs.Wishlist;
using EndProject.Domain.Entitys;
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

        WishlistProduct? wishlistProduct = wishlist.WishlistProducts
                .FirstOrDefault(x => x.CarId == Id && x.WishlistId == wishlist.Id);
        if (wishlistProduct is not null)
        {
            wishlistProduct.Quantity = 1;
        }
        else
        {
            wishlistProduct = new WishlistProduct
            {
                WishlistId = wishlist.Id,
                CarId = Id,
                Quantity = 1
            };
            wishlist.WishlistProducts.Add(wishlistProduct);
        }
        await _wishlistWriteRepository.SavaChangeAsync();
    }


    public async Task DeleteWishlistAsync(Guid id, string AppUserId)
    {
        if (AppUserId is null) throw new NullReferenceException();

        var wishlist = await _wishlistReadRepository
                               .Table
                               .Include(x => x.WishlistProducts)
                               .FirstOrDefaultAsync(x => x.AppUserId == AppUserId);
        if (wishlist is null) throw new NullReferenceException();

        _wishlistWriteRepository.Remove(wishlist);
        await _wishlistWriteRepository.SavaChangeAsync();
    }

    public async Task DeleteWishlistItemAsync(Guid carId, string AppUserId)
    {

        var wishlist = await _wishlistReadRepository
                             .Table
                             .Include(x => x.WishlistProducts)
                             .Include(x => x.AppUser)
                             .FirstOrDefaultAsync(x => x.AppUserId == AppUserId);

        if (wishlist is null) throw new NullReferenceException();

        var basketProduct = wishlist.WishlistProducts.FirstOrDefault(x => x.CarId == carId && x.WishlistId == wishlist.Id);
        if (basketProduct is null) throw new NullReferenceException();

        if (basketProduct.Quantity == 1)
            await _wishlistProductServices.RemoveAsync(basketProduct.Id);

        await _wishlistWriteRepository.SavaChangeAsync();
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
