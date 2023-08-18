using EndProject.Application.Abstraction.Repositories.IEntityRepository;
using EndProject.Domain.Entitys;
using EndProjet.Persistance.Context;

namespace EndProjet.Persistance.Implementations.Repositories.EntityRepository;

public class PickupLocationWriteRepository : WriteRepository<PickupLocation>, IPickupLocationWriteRepository
{
    public PickupLocationWriteRepository(AppDbContext context) : base(context)
    {
    }
}
