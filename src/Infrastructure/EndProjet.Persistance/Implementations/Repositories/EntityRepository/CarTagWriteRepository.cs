using EndProject.Application.Abstraction.Repositories.IEntityRepository;
using EndProject.Domain.Entitys;
using EndProjet.Persistance.Context;

namespace EndProjet.Persistance.Implementations.Repositories.EntityRepository;

public class CarTagWriteRepository : WriteRepository<CarTag>, ICarTagWriteRepository
{
    public CarTagWriteRepository(AppDbContext context) : base(context)
    {
    }
}
