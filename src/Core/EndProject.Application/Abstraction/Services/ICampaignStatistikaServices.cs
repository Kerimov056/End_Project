using EndProject.Application.DTOs.Slider;

namespace EndProject.Application.Abstraction.Services;

public interface ICampaignStatistikaServices
{
    Task<List<SliderGetDTO>> GetAllAsync();
    Task CreateAsync(SliderCreateDTO sliderCreateDTO);
    Task<SliderGetDTO> GetByIdAsync(Guid Id);
    Task UpdateAsync(Guid id, SliderUpdateDTO sliderUptadeDTO);
    Task RemoveAsync(Guid id);
}
