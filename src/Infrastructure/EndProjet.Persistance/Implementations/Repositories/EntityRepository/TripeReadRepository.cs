using EndProject.Application.Abstraction.Repositories.IEntityRepository;
using EndProject.Domain.Entitys;
using EndProjet.Persistance.Context;
using Microsoft.EntityFrameworkCore;

namespace EndProjet.Persistance.Implementations.Repositories.EntityRepository;

public class TripeReadRepository : ReadRepository<Trip>, ITripeReadRepository
{
    private readonly AppDbContext _appDbContext;
    public TripeReadRepository(AppDbContext context) : base(context)
     =>  _appDbContext = context;


    public async Task<int> MyTripCount(string appUserId)
    {
        var byUserTripCount = await _appDbContext.Trips.Where(x=>x.AppUserId==appUserId).CountAsync();
        return byUserTripCount;
    }
}
