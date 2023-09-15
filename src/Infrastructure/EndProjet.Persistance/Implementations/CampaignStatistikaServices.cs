using EndProject.Application.Abstraction.Repositories.IEntityRepository;
using EndProject.Application.Abstraction.Services;
using EndProject.Application.DTOs.CampaignStatistika;

namespace EndProjet.Persistance.Implementations
{
    public class CampaignStatistikaServices : ICampaignStatistikaServices
    {
        private readonly ICampaignStatistikaReadRepository _readRepository;
        private readonly ICampaignStatistikaWriteRepository _writeRepository;

        public CampaignStatistikaServices(ICampaignStatistikaReadRepository readRepository,
                                          ICampaignStatistikaWriteRepository writeRepository)
        {
            _readRepository = readRepository;
            _writeRepository = writeRepository;
        }

        public Task CreateAsync(CampaignStatistikaCreateDTO campaignStatistikaCreateDTO)
        {
            throw new NotImplementedException();
        }

        public Task<List<CampaignStatistikaGetDTO>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<CampaignStatistikaGetDTO> GetByIdAsync(Guid Id)
        {
            throw new NotImplementedException();
        }

        public Task RemoveAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
