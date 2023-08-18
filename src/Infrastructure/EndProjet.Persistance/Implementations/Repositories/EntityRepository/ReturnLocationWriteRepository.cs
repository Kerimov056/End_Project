using EndProject.Application.Abstraction.Repositories.IEntityRepository;
using EndProject.Domain.Entitys;
using EndProjet.Persistance.Context;

namespace EndProjet.Persistance.Implementations.Repositories.EntityRepository;

public class ReturnLocationWriteRepository : WriteRepository<ReturnLocation>, IReturnLocationWriteRepository
{
    public ReturnLocationWriteRepository(AppDbContext context) : base(context)
    {
    }
}
