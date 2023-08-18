using EndProject.Application.Abstraction.Repositories.IEntityRepository;
using EndProject.Domain.Entitys;
using EndProjet.Persistance.Context;

namespace EndProjet.Persistance.Implementations.Repositories.EntityRepository;

public class CarCommentWriteRepository : WriteRepository<CarComment>, ICarCommentWriteRepository
{
    public CarCommentWriteRepository(AppDbContext context) : base(context)
    {
    }
}
