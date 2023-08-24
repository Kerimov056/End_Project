using EndProject.Domain.Entitys;

namespace EndProject.Application.Abstraction.Repositories.IEntityRepository;

public interface ICarReservationReadRepository: IReadRepository<CarReservation>
{
    Task<int> GetReservConfirmedCountAsync();
}
