using EndProject.Application.Abstraction.Repositories.IEntityRepository;
using EndProject.Domain.Entitys;
using EndProjet.Persistance.Context;

namespace EndProjet.Persistance.Implementations.Repositories.EntityRepository;

public class CarWriteRepository : WriteRepository<Car>, ICarWriteRepository
{
    public CarWriteRepository(AppDbContext context) : base(context)
    {
    }
}
