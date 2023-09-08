using EndProject.Application.Abstraction.Repositories.IEntityRepository;
using EndProject.Domain.Entitys;
using EndProjet.Persistance.Context;

namespace EndProjet.Persistance.Implementations.Repositories.EntityRepository;

public class UserMessageWriteRepository : WriteRepository<SendUserMessage>, IUserMessageWriteRepository
{
    public UserMessageWriteRepository(AppDbContext context) : base(context)
    {
    }
}
