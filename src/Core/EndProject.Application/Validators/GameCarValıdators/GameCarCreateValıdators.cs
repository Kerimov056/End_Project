using EndProject.Domain.Entitys;
using FluentValidation;

namespace EndProject.Application.Validators.GameCarValıdators;

public class GameCarCreateValıdators : AbstractValidator<GameCar>
{
    public GameCarCreateValıdators()
    {
        RuleFor(x => x.Password).NotNull().NotEmpty().MaximumLength(60);
    }
}
