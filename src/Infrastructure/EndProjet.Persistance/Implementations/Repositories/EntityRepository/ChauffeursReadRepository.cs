using EndProject.Application.Abstraction.Repositories.IEntityRepository;
using EndProject.Domain.Entitys;
using EndProjet.Persistance.Context;

namespace EndProjet.Persistance.Implementations.Repositories.EntityRepository;

public class ChauffeursReadRepository : ReadRepository<Chauffeurs>, IChauffeursReadRepository
{
    public ChauffeursReadRepository(AppDbContext context) : base(context)
    {
    }
}
