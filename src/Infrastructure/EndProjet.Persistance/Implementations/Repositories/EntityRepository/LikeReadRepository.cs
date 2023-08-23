using EndProject.Application.Abstraction.Repositories.IEntityRepository;
using EndProject.Domain.Entitys;
using EndProjet.Persistance.Context;

namespace EndProjet.Persistance.Implementations.Repositories.EntityRepository;

public class LikeReadRepository : ReadRepository<Like>, ILikeReadRepository
{
    public LikeReadRepository(AppDbContext context) : base(context)
    {
    }
}
