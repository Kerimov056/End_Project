using EndProject.Application.Abstraction.Repositories.IEntityRepository;
using EndProject.Domain.Entitys;
using EndProjet.Persistance.Context;

namespace EndProjet.Persistance.Implementations.Repositories.EntityRepository;

public class TripeReadRepository : ReadRepository<Trip>, ITripeReadRepository
{
    public TripeReadRepository(AppDbContext context) : base(context)
    {
    }
}
