using EndProject.Application.Abstraction.Repositories.IEntityRepository;
using EndProject.Domain.Entitys;
using EndProjet.Persistance.Context;

namespace EndProjet.Persistance.Implementations.Repositories.EntityRepository;

public class OtherCarReservationReadRepository : ReadRepository<OtherCarReservation>, IOtherCarReservationReadRepository
{
    public OtherCarReservationReadRepository(AppDbContext context) : base(context)
    {
    }
}
