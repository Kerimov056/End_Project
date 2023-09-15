using EndProject.Application.Abstraction.Repositories.IEntityRepository;
using EndProject.Domain.Entitys;
using EndProjet.Persistance.Context;

namespace EndProjet.Persistance.Implementations.Repositories.EntityRepository;

public class CampaignStatistikaReadRepository : ReadRepository<CampaignStatistika>, ICampaignStatistikaReadRepository
{
    public CampaignStatistikaReadRepository(AppDbContext context) : base(context)
    {
    }
}
