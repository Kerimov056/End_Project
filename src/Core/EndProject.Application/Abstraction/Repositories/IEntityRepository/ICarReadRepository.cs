using EndProject.Domain.Entitys;

namespace EndProject.Application.Abstraction.Repositories.IEntityRepository;

public interface ICarReadRepository:IReadRepository<Car>
{
    Task<int> GetCarCountAsync();
}
