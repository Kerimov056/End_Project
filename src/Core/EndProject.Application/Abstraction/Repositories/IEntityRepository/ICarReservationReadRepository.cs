using EndProject.Domain.Entitys;

namespace EndProject.Application.Abstraction.Repositories.IEntityRepository;

public interface ICarReservationReadRepository: IReadRepository<CarReservation>
{
    Task<int> GetReservPeddingCountAsync();
    Task<int> GetReservConfirmedCountAsync();
    Task<int> GetReservCompletedCountAsync();
    Task<int> GetReservCanceledCountAsync();
}
