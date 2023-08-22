using EndProject.Application.Abstraction.Repositories.IEntityRepository;
using EndProject.Domain.Entitys;
using EndProjet.Persistance.Context;

namespace EndProjet.Persistance.Implementations.Repositories.EntityRepository;

public class BasketProductReadRepository : ReadRepository<BasketProduct>, IBasketProductReadRepository
{
    public BasketProductReadRepository(AppDbContext context) : base(context)
    {
    }
}
