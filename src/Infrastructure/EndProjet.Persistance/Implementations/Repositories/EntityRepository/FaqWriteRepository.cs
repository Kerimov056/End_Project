using EndProject.Application.Abstraction.Repositories.IEntityRepository;
using EndProject.Domain.Entitys;
using EndProjet.Persistance.Context;

namespace EndProjet.Persistance.Implementations.Repositories.EntityRepository;

public class FaqWriteRepository : WriteRepository<Faq>, IFaqWriteRepository
{
    public FaqWriteRepository(AppDbContext context) : base(context)
    {
    }
}
