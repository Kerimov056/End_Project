using EndProject.Application.Abstraction.Repositories.IEntityRepository;
using EndProject.Domain.Entitys;
using EndProjet.Persistance.Context;
using Microsoft.EntityFrameworkCore;

namespace EndProjet.Persistance.Implementations.Repositories.EntityRepository;

public class CarReadRepository : ReadRepository<Car>, ICarReadRepository
{
 
    private readonly AppDbContext _appDbContext;
    public CarReadRepository(AppDbContext context) : base(context)
    {
        _appDbContext = context;
    }

    public async Task<int> GetCarCountAsync()
    {
        return await _appDbContext.Cars.CountAsync();
    }

    public async Task<int> GetReservCarCountAsync()
    {
        return await _appDbContext.Cars.Where(x => x.isReserv == true).CountAsync();
    }
}
