using EndProject.Application.DTOs.Wishlist;

namespace EndProject.Application.Abstraction.Services;

public interface IWishlistServices
{
    Task AddWishlistAsync(Guid Id, string AppUserId);
    Task<List<WishlistProductDto>> GetWishlistProductsAsync(string AppUserId);
    Task DeleteBasketAsync(Guid id, string AppUserId);
    Task<int> GetWishlistCountAsync(string AppUserId);
    Task DeleteWishlistItemAsync(Guid carId, string AppUserId);
}