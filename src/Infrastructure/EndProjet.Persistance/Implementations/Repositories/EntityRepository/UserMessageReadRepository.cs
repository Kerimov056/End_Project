using EndProject.Application.Abstraction.Repositories;
using EndProject.Domain.Entitys;
using EndProjet.Persistance.Context;

namespace EndProjet.Persistance.Implementations.Repositories.EntityRepository;

public class UserMessageReadRepository : ReadRepository<SendUserMessage>, IUserMessageReadRepository
{
    public UserMessageReadRepository(AppDbContext context) : base(context)
    {
    }
}
