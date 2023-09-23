using EndProject.Application.Abstraction.Repositories.IEntityRepository;
using EndProject.Domain.Entitys;
using EndProjet.Persistance.Context;

namespace EndProjet.Persistance.Implementations.Repositories.EntityRepository;

public class TripeWriteRepository : WriteRepository<Trip>, ITripeWriteRepository
{
    public TripeWriteRepository(AppDbContext context) : base(context)
    {
    }
}
