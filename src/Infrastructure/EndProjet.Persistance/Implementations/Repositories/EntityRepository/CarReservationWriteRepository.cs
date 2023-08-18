using EndProject.Application.Abstraction.Repositories.IEntityRepository;
using EndProject.Domain.Entitys;
using EndProjet.Persistance.Context;

namespace EndProjet.Persistance.Implementations.Repositories.EntityRepository;

public class CarReservationWriteRepository : WriteRepository<CarReservation>, ICarReservationWriteRepository
{
    public CarReservationWriteRepository(AppDbContext context) : base(context)
    {
    }
}
