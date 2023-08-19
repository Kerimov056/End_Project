using EndProject.Application.Abstraction.Repositories.IEntityRepository;
using EndProject.Domain.Entitys.Identity;
using EndProjet.Persistance.Context;

namespace EndProjet.Persistance.Implementations.Repositories.EntityRepository;

public class AdvantageReadRepository : ReadRepository<Advantage>, IAdvantageReadRepository
{
    public AdvantageReadRepository(AppDbContext context) : base(context)
    {
    }
}
