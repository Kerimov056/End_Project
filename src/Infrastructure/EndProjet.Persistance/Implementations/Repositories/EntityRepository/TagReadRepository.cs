using EndProject.Application.Abstraction.Repositories.IEntityRepository;
using EndProject.Domain.Entitys;
using EndProjet.Persistance.Context;

namespace EndProjet.Persistance.Implementations.Repositories.EntityRepository;

public class TagReadRepository : ReadRepository<Tag>, ITagReadRepository
{
    public TagReadRepository(AppDbContext context) : base(context)
    {
    }
}
