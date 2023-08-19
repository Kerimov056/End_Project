using EndProject.Application.Abstraction.Repositories.IEntityRepository;
using EndProject.Domain.Entitys;
using EndProjet.Persistance.Context;

namespace EndProjet.Persistance.Implementations.Repositories.EntityRepository;

public class BlogWriteRepository : WriteRepository<Blog>, IBlogWriteRepository
{
    public BlogWriteRepository(AppDbContext context) : base(context)
    {
    }
}
