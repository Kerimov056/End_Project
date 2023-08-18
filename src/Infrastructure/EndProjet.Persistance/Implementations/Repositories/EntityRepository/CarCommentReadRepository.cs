using EndProject.Application.Abstraction.Repositories.IEntityRepository;
using EndProject.Domain.Entitys;
using EndProjet.Persistance.Context;

namespace EndProjet.Persistance.Implementations.Repositories.EntityRepository;

public class CarCommentReadRepository : ReadRepository<CarComment>, ICarCommentReadRepository
{
    public CarCommentReadRepository(AppDbContext context) : base(context)
    {
    }
}