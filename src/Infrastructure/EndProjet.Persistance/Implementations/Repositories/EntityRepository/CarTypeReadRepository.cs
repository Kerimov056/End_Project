using EndProject.Application.Abstraction.Repositories.IEntityRepository;
using EndProject.Domain.Entitys;
using EndProjet.Persistance.Context;

namespace EndProjet.Persistance.Implementations.Repositories.EntityRepository;

public class CarTypeReadRepository : ReadRepository<CarType>, ICarTypeReadRepository
{
    public CarTypeReadRepository(AppDbContext context) : base(context)
    {
    }
}
