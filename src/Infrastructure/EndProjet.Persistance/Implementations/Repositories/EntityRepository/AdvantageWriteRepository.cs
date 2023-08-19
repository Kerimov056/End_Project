using EndProject.Application.Abstraction.Repositories.IEntityRepository;
using EndProject.Domain.Entitys.Identity;
using EndProjet.Persistance.Context;

namespace EndProjet.Persistance.Implementations.Repositories.EntityRepository;

public class AdvantageWriteRepository : WriteRepository<Advantage>, IAdvantageWriteRepository
{
    public AdvantageWriteRepository(AppDbContext context) : base(context)
    {
    }
}
