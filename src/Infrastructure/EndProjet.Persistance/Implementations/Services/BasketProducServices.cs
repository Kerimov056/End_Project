using EndProject.Application.Abstraction.Repositories.IEntityRepository;
using EndProject.Application.Abstraction.Services;
using EndProjet.Persistance.Exceptions;

namespace EndProjet.Persistance.Implementations.Services;

public class BasketProducServices : IBasketProducServices
{
    private readonly IBasketProductReadRepository _productReadRepository;
    private readonly IBasketProductWriteRepository _productWriteRepository;

    public BasketProducServices(IBasketProductReadRepository productReadRepository,
                                IBasketProductWriteRepository productWriteRepository)
    {
        _productReadRepository = productReadRepository;
        _productWriteRepository = productWriteRepository;
    }

    public async Task RemoveAsync(Guid id)
    {
        var byProducrt = await _productReadRepository.GetByIdAsync(id);
        if (byProducrt is null) throw new NotFoundException("Produxt is Null");

        _productWriteRepository.Remove(byProducrt);
        await _productWriteRepository.SavaChangeAsync();
    }
}
