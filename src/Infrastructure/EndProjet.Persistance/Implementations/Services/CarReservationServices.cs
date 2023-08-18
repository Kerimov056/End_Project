using EndProject.Application.Abstraction.Services;
using EndProject.Application.DTOs.CarReservation;
using EndProject.Application.DTOs.Slider;

namespace EndProjet.Persistance.Implementations.Services;

public class CarReservationServices : ICarReservationServices
{
    public Task CreateAsync(CarReservationCreateDTO carReservationCreateDTO)
    {
        throw new NotImplementedException();
    }

    public Task<List<CarReservationGetDTO>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<CarReservationGetDTO> GetByIdAsync(Guid Id)
    {
        throw new NotImplementedException();
    }

    public Task RemoveAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(Guid id, CarReservationUpdateDTO carReservationUpdateDTO)
    {
        throw new NotImplementedException();
    }
}
