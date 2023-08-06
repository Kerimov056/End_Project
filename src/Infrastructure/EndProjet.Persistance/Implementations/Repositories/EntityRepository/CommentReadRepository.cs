using EndProject.Application.Abstraction.Repositories.IEntityRepository;
using EndProject.Domain.Entitys;
using EndProjet.Persistance.Context;

namespace EndProjet.Persistance.Implementations.Repositories.EntityRepository;

public class CommentReadRepository : ReadRepository<Comments>, ICommentReadRepository
{
    public CommentReadRepository(AppDbContext context) : base(context)
    {
    }
}
