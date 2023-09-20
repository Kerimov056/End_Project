using EndProject.Application.Abstraction.Repositories.IEntityRepository;
using EndProject.Domain.Entitys;
using EndProjet.Persistance.Context;

namespace EndProjet.Persistance.Implementations.Repositories.EntityRepository;

public class GameCarWriteRepository : WriteRepository<GameCar>, IGameCarWriteRepository
{
    public GameCarWriteRepository(AppDbContext context) : base(context)
    {
    }
}
