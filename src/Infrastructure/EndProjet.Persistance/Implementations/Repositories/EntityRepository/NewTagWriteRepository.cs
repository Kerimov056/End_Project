using EndProject.Application.Abstraction.Repositories.IEntityRepository;
using EndProject.Domain.Entitys;
using EndProjet.Persistance.Context;

namespace EndProjet.Persistance.Implementations.Repositories.EntityRepository;

public class NewTagWriteRepository : WriteRepository<NewTag>, INewTagWriteRepository
{
    public NewTagWriteRepository(AppDbContext context) : base(context)
    {
    }
}
