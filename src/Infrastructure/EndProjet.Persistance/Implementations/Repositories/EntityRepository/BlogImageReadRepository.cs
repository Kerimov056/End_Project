using EndProject.Application.Abstraction.Repositories.IEntityRepository;
using EndProject.Domain.Entitys;
using EndProjet.Persistance.Context;

namespace EndProjet.Persistance.Implementations.Repositories.EntityRepository;

public class BlogImageReadRepository : ReadRepository<BlogImage>, IBlogImageReadRepository
{
    public BlogImageReadRepository(AppDbContext context) : base(context)
    {
    }
}
