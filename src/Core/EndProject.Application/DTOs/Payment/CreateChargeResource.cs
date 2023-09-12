namespace EndProject.Application.DTOs.Payment;

public record CreateChargeResource(
    string Currency,
    long Amount,
    string CustomerId,
    string Email,
    string Description);