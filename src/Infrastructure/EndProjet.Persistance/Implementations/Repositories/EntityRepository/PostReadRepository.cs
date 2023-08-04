using EndProject.Application.Abstraction.Repositories.IEntityRepository;
using EndProject.Domain.Entitys;
using EndProjet.Persistance.Context;

namespace EndProjet.Persistance.Implementations.Repositories.EntityRepository;

public class PostReadRepository : ReadRepository<Posts>, IPostReadRepository
{
    public PostReadRepository(AppDbContext context) : base(context)
    {
    }
}
