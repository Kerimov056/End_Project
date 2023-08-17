using EndProject.Application.Abstraction.Repositories.IEntityRepository;
using EndProject.Domain.Entitys;
using EndProjet.Persistance.Context;

namespace EndProjet.Persistance.Implementations.Repositories.EntityRepository;

public class CarTypeWriteRepository : WriteRepository<CarType>, ICarTypeWriteRepository
{
    public CarTypeWriteRepository(AppDbContext context) : base(context)
    {
    }
}
