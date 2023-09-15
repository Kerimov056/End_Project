using EndProject.Application.DTOs.CampaignStatistika;

namespace EndProject.Application.Abstraction.Services;

public interface ICampaignStatistikaServices
{
    Task<List<CampaignStatistikaGetDTO>> GetAllAsync();
    Task CreateAsync(CampaignStatistikaCreateDTO campaignStatistikaCreateDTO);
    Task<CampaignStatistikaGetDTO> GetByIdAsync(Guid Id);
    Task UpdateAsync(Guid id);
    Task RemoveAsync(Guid id);
}
