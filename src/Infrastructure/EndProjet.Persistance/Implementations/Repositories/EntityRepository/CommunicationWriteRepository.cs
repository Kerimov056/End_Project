using EndProject.Application.Abstraction.Repositories.IEntityRepository;
using EndProject.Domain.Entitys;
using EndProjet.Persistance.Context;

namespace EndProjet.Persistance.Implementations.Repositories.EntityRepository;

public class CommunicationWriteRepository : WriteRepository<Communication>, ICommunicationWriteRepository
{
    public CommunicationWriteRepository(AppDbContext context) : base(context)
    {
    }
}
