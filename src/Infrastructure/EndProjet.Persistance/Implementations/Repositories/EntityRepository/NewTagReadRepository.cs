using EndProject.Application.Abstraction.Repositories.IEntityRepository;
using EndProject.Domain.Entitys;
using EndProjet.Persistance.Context;

namespace EndProjet.Persistance.Implementations.Repositories.EntityRepository;

public class NewTagReadRepository : ReadRepository<NewTag>, INewTagReadRepository
{
    public NewTagReadRepository(AppDbContext context) : base(context)
    {
    }
}
