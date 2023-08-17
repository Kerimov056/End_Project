using EndProject.Application.Abstraction.Repositories.IEntityRepository;
using EndProject.Domain.Entitys;
using EndProjet.Persistance.Context;

namespace EndProjet.Persistance.Implementations.Repositories.EntityRepository;

public class CarCategoryReadRepository : ReadRepository<CarCategory>, ICarCategoryReadRepository
{
    public CarCategoryReadRepository(AppDbContext context) : base(context)
    {
    }
}
