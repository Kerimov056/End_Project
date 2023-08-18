using EndProject.Application.Abstraction.Repositories.IEntityRepository;
using EndProject.Domain.Entitys;
using EndProjet.Persistance.Context;

namespace EndProjet.Persistance.Implementations.Repositories.EntityRepository;

public class ReturnLocationReadRepository : ReadRepository<ReturnLocation>, IReturnLocationReadRepository
{
    public ReturnLocationReadRepository(AppDbContext context) : base(context)
    {
    }
}
