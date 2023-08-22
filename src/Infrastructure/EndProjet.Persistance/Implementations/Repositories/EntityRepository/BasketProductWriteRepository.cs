using EndProject.Application.Abstraction.Repositories.IEntityRepository;
using EndProject.Domain.Entitys;
using EndProjet.Persistance.Context;

namespace EndProjet.Persistance.Implementations.Repositories.EntityRepository;

public class BasketProductWriteRepository : WriteRepository<BasketProduct>, IBasketProductWriteRepository
{
    public BasketProductWriteRepository(AppDbContext context) : base(context)
    {
    }
}
