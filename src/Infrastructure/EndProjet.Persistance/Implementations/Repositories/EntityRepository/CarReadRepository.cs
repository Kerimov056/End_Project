using EndProject.Application.Abstraction.Repositories.IEntityRepository;
using EndProject.Domain.Entitys;
using EndProjet.Persistance.Context;

namespace EndProjet.Persistance.Implementations.Repositories.EntityRepository;

public class CarReadRepository : ReadRepository<Car>, ICarReadRepository
{
    public CarReadRepository(AppDbContext context) : base(context)
    {
    }
}
