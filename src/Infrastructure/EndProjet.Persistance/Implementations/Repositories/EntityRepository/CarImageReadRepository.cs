using EndProject.Application.Abstraction.Repositories.IEntityRepository;
using EndProject.Domain.Entitys;
using EndProjet.Persistance.Context;

namespace EndProjet.Persistance.Implementations.Repositories.EntityRepository;

public class CarImageReadRepository : ReadRepository<CarImage>, ICarImageReadRepository
{
    public CarImageReadRepository(AppDbContext context) : base(context)
    {
    }
}
