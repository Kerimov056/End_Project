using EndProject.Application.Abstraction.Repositories.IEntityRepository;
using EndProject.Domain.Entitys;
using EndProjet.Persistance.Context;

namespace EndProjet.Persistance.Implementations.Repositories.EntityRepository;

public class FaqReadRepository : ReadRepository<Faq>, IFaqReadRepository
{
    public FaqReadRepository(AppDbContext context) : base(context)
    {
    }
}
