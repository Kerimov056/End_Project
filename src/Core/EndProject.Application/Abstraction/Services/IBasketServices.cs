using EndProject.Application.DTOs.Basket;

namespace EndProject.Application.Abstraction.Services;

public interface IBasketServices
{
    Task AddBasketAsync(Guid Id);
    Task<List<BasketProductListDto>> GetBasketProductsAsync();
    Task DeleteBasketAsync(Guid id);
    Task<int> GetBasketCountAsync();
    Task DeleteBasketItemAsync(Guid carId);
}
