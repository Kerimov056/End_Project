using EndProject.Application.Abstraction.Repositories.IEntityRepository;
using EndProject.Domain.Entitys;
using EndProjet.Persistance.Context;

namespace EndProjet.Persistance.Implementations.Repositories.EntityRepository;

public class PostImageWriteRepository : WriteRepository<PostImage>, IPostImageWriteRepository
{
    public PostImageWriteRepository(AppDbContext context) : base(context)
    {
    }
}
