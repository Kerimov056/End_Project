using EndProject.Application.Abstraction.Repositories.IEntityRepository;
using EndProject.Domain.Entitys;
using EndProjet.Persistance.Context;

namespace EndProjet.Persistance.Implementations.Repositories.EntityRepository;

public class OtherCarReservationWriteRepository : WriteRepository<OtherCarReservation>, IOtherCarReservationWriteRepository
{
    public OtherCarReservationWriteRepository(AppDbContext context) : base(context)
    {
    }
}
