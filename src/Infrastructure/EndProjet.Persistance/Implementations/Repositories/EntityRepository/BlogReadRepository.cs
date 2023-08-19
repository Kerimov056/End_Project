using EndProject.Application.Abstraction.Repositories.IEntityRepository;
using EndProject.Domain.Entitys;
using EndProjet.Persistance.Context;

namespace EndProjet.Persistance.Implementations.Repositories.EntityRepository;

public class BlogReadRepository : ReadRepository<Blog>, IBlogReadRepository
{
    public BlogReadRepository(AppDbContext context) : base(context)
    {
    }
}
