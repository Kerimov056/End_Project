using EndProject.Application.Abstraction.Repositories.IEntityRepository;
using EndProject.Domain.Entitys;
using EndProjet.Persistance.Context;

namespace EndProjet.Persistance.Implementations.Repositories.EntityRepository;

public class CarReservationReadRepository : ReadRepository<CarReservation>, ICarReservationReadRepository
{
    public CarReservationReadRepository(AppDbContext context) : base(context)
    {
    }
}
