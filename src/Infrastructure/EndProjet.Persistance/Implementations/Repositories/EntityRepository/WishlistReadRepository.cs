using EndProject.Application.Abstraction.Repositories.IEntityRepository;
using EndProject.Domain.Entitys;
using EndProjet.Persistance.Context;

namespace EndProjet.Persistance.Implementations.Repositories.EntityRepository;

public class WishlistReadRepository : ReadRepository<Wishlist>, IWishlistReadRepository
{
    public WishlistReadRepository(AppDbContext context) : base(context)
    {
    }
}
