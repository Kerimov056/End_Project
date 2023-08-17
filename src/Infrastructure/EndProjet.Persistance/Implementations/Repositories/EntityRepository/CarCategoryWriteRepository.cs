using EndProject.Application.Abstraction.Repositories.IEntityRepository;
using EndProject.Domain.Entitys;
using EndProjet.Persistance.Context;

namespace EndProjet.Persistance.Implementations.Repositories.EntityRepository;

public class CarCategoryWriteRepository : WriteRepository<CarCategory>, ICarCategoryWriteRepository
{
    public CarCategoryWriteRepository(AppDbContext context) : base(context)
    {
    }
}
