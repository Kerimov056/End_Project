using EndProject.Application.Abstraction.Repositories.IEntityRepository;
using EndProject.Application.Abstraction.Services;
using EndProjet.Persistance.Exceptions;

namespace EndProjet.Persistance.Implementations.Services;

public class WishlistProductServices : IWishlistProductServices
{
    private readonly IWishlistProductReadRepository _wishlistProductReadRepository;
    private readonly IWishlistProductWriteRepository _wishlistProductWriteRepository;

    public WishlistProductServices(IWishlistProductReadRepository wishlistProductReadRepository,
                                   IWishlistProductWriteRepository wishlistProductWriteRepository)
    {
        _wishlistProductReadRepository = wishlistProductReadRepository;
        _wishlistProductWriteRepository = wishlistProductWriteRepository;
    }

    public async Task RemoveAsync(Guid id)
    {
        var byProduct = await _wishlistProductReadRepository.GetByIdAsync(id);
        if (byProduct is null) throw new NotFoundException("Produst is Null");

        _wishlistProductWriteRepository.Remove(byProduct);
        await _wishlistProductWriteRepository.SavaChangeAsync();
    }
}
