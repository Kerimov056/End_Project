using AutoMapper;
using EndProject.Application.DTOs.Game;
using EndProject.Domain.Entitys;

namespace EndProjet.Persistance.MapperProfiles;

public class GameCarProfile : Profile
{
    public GameCarProfile()
    {
        CreateMap<GameCar, GameCarCreateDTO>().ReverseMap();
        CreateMap<GameCar, GameCarGetDTO>().ReverseMap();
    }
}
