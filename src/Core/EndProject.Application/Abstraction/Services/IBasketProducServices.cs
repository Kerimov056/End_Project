using EndProject.Application.DTOs.CarReservation;

namespace EndProject.Application.Abstraction.Services;

public interface IBasketProducServices
{
    Task RemoveAsync(Guid id);
}
