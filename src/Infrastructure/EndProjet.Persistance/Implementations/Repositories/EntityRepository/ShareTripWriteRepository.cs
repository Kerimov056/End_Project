using EndProject.Application.Abstraction.Repositories.IEntityRepository;
using EndProject.Domain.Entitys;
using EndProjet.Persistance.Context;

namespace EndProjet.Persistance.Implementations.Repositories.EntityRepository;

public class ShareTripWriteRepository : WriteRepository<ShareTrip>, IShareTripWriteRepository
{
    public ShareTripWriteRepository(AppDbContext context) : base(context)
    {
    }
}
