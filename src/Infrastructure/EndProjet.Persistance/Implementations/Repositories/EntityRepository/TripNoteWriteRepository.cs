using EndProject.Application.Abstraction.Repositories.IEntityRepository;
using EndProject.Domain.Entitys;
using EndProjet.Persistance.Context;

namespace EndProjet.Persistance.Implementations.Repositories.EntityRepository;

public class TripNoteWriteRepository : WriteRepository<TripNote>, ITripNoteWriteRepository
{
    public TripNoteWriteRepository(AppDbContext context) : base(context)
    {
    }
}
