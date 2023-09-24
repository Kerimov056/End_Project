using EndProject.Application.Abstraction.Repositories.IEntityRepository;
using EndProject.Domain.Entitys;
using EndProjet.Persistance.Context;

namespace EndProjet.Persistance.Implementations.Repositories.EntityRepository;

public class TripNoteReadRepository : ReadRepository<TripNote>, ITripNoteReadRepository
{
    public TripNoteReadRepository(AppDbContext context) : base(context)
    {
    }
}
