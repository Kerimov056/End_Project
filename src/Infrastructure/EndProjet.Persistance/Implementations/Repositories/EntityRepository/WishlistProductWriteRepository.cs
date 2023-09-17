using EndProject.Application.Abstraction.Repositories.IEntityRepository;
using EndProject.Domain.Entitys;
using EndProjet.Persistance.Context;

namespace EndProjet.Persistance.Implementations.Repositories.EntityRepository;

public class WishlistProductWriteRepository : WriteRepository<WishlistProduct>, IWishlistProductWriteRepository
{
    public WishlistProductWriteRepository(AppDbContext context) : base(context)
    {
    }
}
