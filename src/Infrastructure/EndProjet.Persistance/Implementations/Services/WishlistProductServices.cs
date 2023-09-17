using EndProject.Application.Abstraction.Repositories.IEntityRepository;
using EndProject.Application.Abstraction.Services;

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

    public Task RemoveAsync(Guid id)
    {
        throw new NotImplementedException();
    }
}
