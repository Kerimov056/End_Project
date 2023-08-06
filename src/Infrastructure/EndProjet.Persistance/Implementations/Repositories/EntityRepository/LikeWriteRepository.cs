using EndProject.Application.Abstraction.Repositories.IEntityRepository;
using EndProject.Domain.Entitys;
using EndProjet.Persistance.Context;

namespace EndProjet.Persistance.Implementations.Repositories.EntityRepository;

public class LikeWriteRepository : WriteRepository<Like>, ILikeWriteRepository
{
    public LikeWriteRepository(AppDbContext context) : base(context)
    {
    }
}
