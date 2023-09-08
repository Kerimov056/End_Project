using EndProject.Application.DTOs.CarReservation;

namespace EndProject.Application.Abstraction.Services;

public interface ISendUserMessageServices
{
    Task ByUserEmailMessage(UserEmailMessageDTO userEmailMessage);
}
