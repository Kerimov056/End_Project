using EndProject.Application.Abstraction.Services;
using EndProject.Application.DTOs.Slider;

namespace EndProjet.Persistance.Implementations.Services;

public class CarReservationServices : ICarReservationServices
{
    public Task CreateAsync(SliderCreateDTO sliderCreateDTO)
    {
        throw new NotImplementedException();
    }

    public Task<List<SliderGetDTO>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<SliderGetDTO> GetByIdAsync(Guid Id)
    {
        throw new NotImplementedException();
    }

    public Task RemoveAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(Guid id, SliderUpdateDTO sliderUptadeDTO)
    {
        throw new NotImplementedException();
    }
}
