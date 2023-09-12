using AutoMapper;
using EndProject.Application.Abstraction.Repositories.IEntityRepository;
using EndProject.Application.Abstraction.Services;
using EndProject.Application.DTOs.Basket;
using EndProject.Domain.Entitys;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace EndProjet.Persistance.Implementations.Services;

public class BasketServices : IBasketServices
{
    private readonly IBasketReadRepository _readRepository;
    private readonly IBasketWriteRepository _writeRepository;
    private readonly IMapper _mapper;
    private readonly IHttpContextAccessor _contextAccessor;
    private readonly IBasketProducServices _productsServices;
    private readonly ICarServices _carServices;

    public BasketServices(IBasketReadRepository readRepository,
                          IBasketWriteRepository writeRepository,
                          IHttpContextAccessor contextAccessor,
                          IMapper mapper,
                          IBasketProducServices basketProducServices,
                          ICarServices carServices)
    {
        _readRepository = readRepository;
        _writeRepository = writeRepository;
        _contextAccessor = contextAccessor;
        _productsServices = basketProducServices;
        _mapper = mapper;
        _carServices = carServices;
    }

    public async Task AddBasketAsync(Guid Id, string UserId)
    {
        //var userId = "4d12fb35-a688-4270-9c51-f28d4b19e3ae";

        var basket = await _readRepository
                            .Table
                            .Include(x => x.basketProduct)
                            .FirstOrDefaultAsync(x => x.AppUserId == UserId);
        if (basket is null)
        {
            basket = new Basket
            {
                AppUserId = UserId,
            };
            await _writeRepository.AddAsync(basket);
            await _writeRepository.SavaChangeAsync();
        }

        var basketProduct = basket.basketProduct.FirstOrDefault(x => x.CarId == Id && x.BasketId == basket.Id);
        if (basketProduct is not null) basketProduct.Quantity = 1;
        else
        {
            basketProduct = new BasketProduct
            {
                BasketId = basket.Id,
                CarId = Id,
                Quantity = 1
            };
            basket.basketProduct.Add(basketProduct);
        }
        await _writeRepository.SavaChangeAsync();
    }

    public async Task DeleteBasketAsync(Guid id, string UserId)
    {
        //var userId = "4d12fb35-a688-4270-9c51-f28d4b19e3ae";
        if (UserId is null) throw new NullReferenceException();

        var basket = await _readRepository
                               .Table
                               .Include(x =>x.basketProduct)
                               .FirstOrDefaultAsync(x=>x.AppUserId == UserId);
        if (basket == null) throw new NullReferenceException();

        _writeRepository.Remove(basket);
        await _writeRepository.SavaChangeAsync();
    }

    public async Task DeleteBasketItemAsync(Guid carId, string AppUserId)
    {
        //var userId = "4d12fb35-a688-4270-9c51-f28d4b19e3ae";

        var basket = await _readRepository
                             .Table
                             .Include(x => x.basketProduct)
                             .Include(x => x.AppUser)
                             .FirstOrDefaultAsync(x => x.AppUserId == AppUserId);

        if (basket is null) throw new NullReferenceException();

        var basketProduct = basket.basketProduct.FirstOrDefault(x => x.CarId == carId && x.BasketId == basket.Id);
        if (basketProduct is null) throw new NullReferenceException();

        if (basketProduct.Quantity == 1)
            await _productsServices.RemoveAsync(basketProduct.Id);

        await _writeRepository.SavaChangeAsync();
    }

    public async Task<int> GetBasketCountAsync(string UserId)
    {

        var basket = await _readRepository
                            .Table
                            .Include(x => x.basketProduct)
                            .FirstOrDefaultAsync(x => x.AppUserId == UserId);
        if (basket == null) throw new NullReferenceException();

        var basketProduct = basket?.basketProduct;
        var uniqeProduct = basketProduct.GroupBy(m => m.Id)
                                        .Select(x => x.First())
                                        .ToList();

        var uniqeProductCount = uniqeProduct?.Count() ?? 0;
        return uniqeProductCount;
    }

    public async Task<List<BasketProductListDto>> GetBasketProductsAsync(string UserId)
    {

        var basket = await _readRepository
                            .Table
                            .Include(x => x.basketProduct)
                            .FirstOrDefaultAsync(x => x.AppUserId == UserId);
        if (basket == null) throw new NullReferenceException();

        var basketProduct = _mapper.Map<List<BasketProductListDto>>(basket.basketProduct);
        foreach (var byCar in basketProduct)
        {
            byCar.carGetDTO = await _carServices.GetByIdAsync(byCar.CarId);
        }
        return basketProduct;    
    }

    private async Task<int> GetItemBasketCount(Guid carId)
    {
        var userId = "4d12fb35-a688-4270-9c51-f28d4b19e3ae";

        var basket = await _readRepository
                           .Table
                           .Include(x => x.basketProduct)
                           .FirstOrDefaultAsync(x => x.AppUserId == userId);
        if (basket == null) throw new NullReferenceException();

        var basketProduct = basket?.basketProduct.FirstOrDefault(x=>x.CarId == carId);
        if (basketProduct is null) return 0;
        var productQuantoty = basketProduct.Quantity;
        return productQuantoty;
    }
}
