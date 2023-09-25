using EndProject.Application.Abstraction.Repositories.IEntityRepository;
using EndProject.Domain.Entitys;
using EndProjet.Persistance.Context;

namespace EndProjet.Persistance.Implementations.Repositories.EntityRepository;

public class ShareTripReadRepository : ReadRepository<ShareTrip>, IShareTripReadRepository
{
    public ShareTripReadRepository(AppDbContext context) : base(context)
    {
    }
}
