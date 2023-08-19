using EndProject.Application.Abstraction.Repositories.IEntityRepository;
using EndProject.Domain.Entitys;
using EndProjet.Persistance.Context;

namespace EndProjet.Persistance.Implementations.Repositories.EntityRepository;

public class BlogImageWriteRepository : WriteRepository<BlogImage>, IBlogImageWriteRepository
{
    public BlogImageWriteRepository(AppDbContext context) : base(context)
    {
    }
}
