using EndProject.Application.DTOs.Slider;

namespace EndProject.Application.Abstraction.Services;

public interface ISliderService
{
    Task<List<SliderGetDTO>> GetAllAsync();
    Task CreateAsync(SliderCreateDTO sliderCreateDTO);
    Task<SliderGetDTO> GetByIdAsync(Guid Id);
    Task UpdateAsync(Guid id, SliderUpdateDTO sliderUptadeDTO);
    Task RemoveAsync(Guid id);

    //QRCoder
    Task<byte[]> GetQRCOdoerSlider(Guid Id);
}
