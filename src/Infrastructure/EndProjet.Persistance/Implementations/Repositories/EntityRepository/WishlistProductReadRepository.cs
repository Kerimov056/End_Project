using EndProject.Application.Abstraction.Repositories.IEntityRepository;
using EndProject.Domain.Entitys;
using EndProjet.Persistance.Context;

namespace EndProjet.Persistance.Implementations.Repositories.EntityRepository;

public class WishlistProductReadRepository : ReadRepository<WishlistProduct>, IWishlistProductReadRepository
{
    public WishlistProductReadRepository(AppDbContext context) : base(context)
    {
    }
}
