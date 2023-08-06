using EndProject.Application.Abstraction.Repositories.IEntityRepository;
using EndProject.Domain.Entitys;
using EndProjet.Persistance.Context;

namespace EndProjet.Persistance.Implementations.Repositories.EntityRepository;

public class CommentWriteRepository : WriteRepository<Comments>, ICommentWriteRepository
{
    public CommentWriteRepository(AppDbContext context) : base(context)
    {
    }
}
