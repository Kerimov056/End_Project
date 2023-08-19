using EndProject.Application.Abstraction.Repositories.IEntityRepository;
using EndProject.Domain.Entitys;
using EndProjet.Persistance.Context;

namespace EndProjet.Persistance.Implementations.Repositories.EntityRepository;

public class ChauffeursWriteRepository : WriteRepository<Chauffeurs>, IChauffeursWriteRepository
{
    public ChauffeursWriteRepository(AppDbContext context) : base(context)
    {
    }
}
