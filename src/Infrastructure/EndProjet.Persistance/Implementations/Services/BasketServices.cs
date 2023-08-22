﻿using AutoMapper;
using EndProject.Application.Abstraction.Repositories.IEntityRepository;
using EndProject.Application.Abstraction.Services;
using EndProject.Application.DTOs.Basket;
using EndProject.Domain.Entitys;
using EndProjet.Persistance.Context;
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

    public BasketServices(IBasketReadRepository readRepository,
                          IBasketWriteRepository writeRepository,
                          IHttpContextAccessor contextAccessor,
                          IMapper mapper,
                          IBasketProducServices basketProducServices)
    {
        _readRepository = readRepository;
        _writeRepository = writeRepository;
        _contextAccessor = contextAccessor;
        _productsServices = basketProducServices;
        _mapper = mapper;
    }

    public async Task AddBasketAsync(Guid Id)
    {
        //var userId = _contextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var userId = "1c1b3f62-334d-46c1-8563-b872782f11ed";

        var basket = await _readRepository
                            .Table
                            .Include(x => x.basketProduct)
                            .FirstOrDefaultAsync(x => x.AppUserId == userId);
        if (basket == null)
        {
            basket = new Basket
            {
                AppUserId = userId,
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

    public async Task DeleteBasketAsync(Guid id)
    {
       var userId = "1c1b3f62-334d-46c1-8563-b872782f11ed";
        if (userId is null) throw new NullReferenceException();

        var basket = await _readRepository
                               .Table
                               .Include(x =>x.basketProduct)
                               .FirstOrDefaultAsync(x=>x.AppUserId == userId);
        if (basket == null) throw new NullReferenceException();

        _writeRepository.Remove(basket);
        await _writeRepository.SavaChangeAsync();
    }

    public async Task DeleteBasketItemAsync(Guid carId)
    {
        var userId = "1c1b3f62-334d-46c1-8563-b872782f11ed";

        var basket = await _readRepository
                             .Table
                             .Include(x => x.basketProduct)
                             .FirstOrDefaultAsync(x => x.AppUserId == userId);
        if (basket is null) throw new NullReferenceException();

        var basketProduct = basket.basketProduct.FirstOrDefault(x => x.CarId == carId && x.BasketId == basket.Id);
        if (basketProduct is null) throw new NullReferenceException();

        if (basketProduct.Quantity == 1)
            await _productsServices.RemoveAsync(basketProduct.Id);

        await _writeRepository.SavaChangeAsync();
    }

    public async Task<int> GetBasketCountAsync()
    {
        var userId = "1c1b3f62-334d-46c1-8563-b872782f11ed";

        var basket = await _readRepository
                            .Table
                            .Include(x => x.basketProduct)
                            .FirstOrDefaultAsync(x => x.AppUserId == userId);
        if (basket == null) throw new NullReferenceException();

        var basketProduct = basket?.basketProduct;
        var uniqeProduct = basketProduct.GroupBy(m => m.Id)
                                        .Select(x => x.First())
                                        .ToList();

        var uniqeProductCount = uniqeProduct?.Count() ?? 0;
        return uniqeProductCount;
    }

    public async Task<List<BasketProductListDto>> GetBasketProductsAsync()
    {
        var userId = "1c1b3f62-334d-46c1-8563-b872782f11ed";

        var basket = await _readRepository
                            .Table
                            .Include(x => x.basketProduct)
                            .FirstOrDefaultAsync(x => x.AppUserId == userId);
        if (basket == null) throw new NullReferenceException();

        var basketProduct = _mapper.Map<List<BasketProductListDto>>(basket.basketProduct);
        return basketProduct;
    }

    private async Task<int> GetItemBasketCount(Guid carId)
    {
        var userId = "1c1b3f62-334d-46c1-8563-b872782f11ed";


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
