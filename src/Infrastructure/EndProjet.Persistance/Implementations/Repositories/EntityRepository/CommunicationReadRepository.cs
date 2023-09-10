using EndProject.Application.Abstraction.Repositories.IEntityRepository;
using EndProject.Domain.Entitys;
using EndProjet.Persistance.Context;

namespace EndProjet.Persistance.Implementations.Repositories.EntityRepository;

public class CommunicationReadRepository : ReadRepository<Communication>, ICommunicationReadRepository
{
    public CommunicationReadRepository(AppDbContext context) : base(context)
    {
    }
}
