using EndProject.Application.Abstraction.Repositories.IEntityRepository;
using EndProject.Domain.Entitys;
using EndProjet.Persistance.Context;

namespace EndProjet.Persistance.Implementations.Repositories.EntityRepository;

public class CarImageWriteRepository : WriteRepository<CarImage>, ICarImageWriteRepository
{
    public CarImageWriteRepository(AppDbContext context) : base(context)
    {
    }
}
