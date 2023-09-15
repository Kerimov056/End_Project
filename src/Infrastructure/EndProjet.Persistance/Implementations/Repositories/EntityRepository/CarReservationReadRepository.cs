using EndProject.Application.Abstraction.Repositories.IEntityRepository;
using EndProject.Domain.Entitys;
using EndProject.Domain.Enums.ReservationStatus;
using EndProjet.Persistance.Context;
using Microsoft.EntityFrameworkCore;

namespace EndProjet.Persistance.Implementations.Repositories.EntityRepository;

public class CarReservationReadRepository : ReadRepository<CarReservation>, ICarReservationReadRepository
{
    private readonly AppDbContext _appDbContext;
    public CarReservationReadRepository(AppDbContext context) : base(context)
    {
        _appDbContext = context;
    }

    public async Task<int> GetReservCanceledCountAsync()
    {
        return await _appDbContext.CarReservations
            .Where(x => x.Status == ReservationStatus.Canceled)
            .Where(x => x.IsDeleted == false).CountAsync();
    }

    public async Task<int> GetReservCompletedCountAsync()
    {
        return await _appDbContext.CarReservations
            .Where(x => x.Status == ReservationStatus.Completed)
            .Where(x => x.IsDeleted == false).CountAsync();
    }

    public async Task<int> GetReservConfirmedCountAsync()
    {
        return await _appDbContext.CarReservations
            .Where(x => x.Status == ReservationStatus.Confirmed)
            .Where(x => x.IsDeleted == false).CountAsync();
    }

    public async Task<int> GetReservNowCountAsync()
    {
        return await _appDbContext.CarReservations
            .Where(x => x.Status == ReservationStatus.RightNow)
            .Where(x => x.IsDeleted == false).CountAsync();

    }

    public async Task<int> GetReservPeddingCountAsync()
    {
        return await _appDbContext.CarReservations
            .Where(x => x.Status == ReservationStatus.Pending)
            .Where(x => x.IsDeleted == false).CountAsync();
    }
}
