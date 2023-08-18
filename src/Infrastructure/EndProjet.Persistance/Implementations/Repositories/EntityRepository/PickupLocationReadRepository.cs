using EndProject.Application.Abstraction.Repositories.IEntityRepository;
using EndProject.Domain.Entitys;
using EndProjet.Persistance.Context;

namespace EndProjet.Persistance.Implementations.Repositories.EntityRepository;

public class PickupLocationReadRepository : ReadRepository<PickupLocation>, IPickupLocationReadRepository
{
    public PickupLocationReadRepository(AppDbContext context) : base(context)
    {
    }
}
