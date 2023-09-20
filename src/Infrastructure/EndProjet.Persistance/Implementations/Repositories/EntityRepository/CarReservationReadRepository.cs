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
     =>  _appDbContext = context;

    public async Task<bool> carFindGameAcces(string AppUserId)
    {
        var byUserReservastions = await _appDbContext.CarReservations
                    .Where(x => x.AppUserId == AppUserId)
                    .Where(x => x.Status == ReservationStatus.Completed)
                    .CountAsync();
        if(byUserReservastions == 3) return true;
        return false;
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

    public async Task<int> NotCompaignStaitsik() //d2cb6d2a-0d22-4437-a8fa-29fdaf8b1341
    {
        var CampaignSum = await _appDbContext.CarReservations.CountAsync();

        return CampaignSum;
    }
}
