using EndProject.Application.DTOs.Basket;

namespace EndProject.Application.Abstraction.Services;

public interface IBasketServices
{
    Task AddBasketAsync(Guid Id, string AppUserId);
    Task<List<BasketProductListDto>> GetBasketProductsAsync(string AppUserId);
    Task DeleteBasketAsync(Guid id, string AppUserId);
    Task<int> GetBasketCountAsync(string AppUserId);
    Task DeleteBasketItemAsync(Guid carId);
}
