using EndProject.Application.Abstraction.Repositories.IEntityRepository;
using EndProject.Domain.Entitys;
using EndProjet.Persistance.Context;

namespace EndProjet.Persistance.Implementations.Repositories.EntityRepository;

public class TagWriteRepository : WriteRepository<Tag>, ITagWriteRepository
{
    public TagWriteRepository(AppDbContext context) : base(context)
    {
    }
}
