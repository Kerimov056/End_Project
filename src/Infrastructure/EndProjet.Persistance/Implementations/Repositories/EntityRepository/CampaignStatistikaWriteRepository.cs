using EndProject.Application.Abstraction.Repositories.IEntityRepository;
using EndProject.Domain.Entitys;
using EndProjet.Persistance.Context;

namespace EndProjet.Persistance.Implementations.Repositories.EntityRepository;

public class CampaignStatistikaWriteRepository : WriteRepository<CampaignStatistika>, ICampaignStatistikaWriteRepository
{
    public CampaignStatistikaWriteRepository(AppDbContext context) : base(context)
    {
    }
}
