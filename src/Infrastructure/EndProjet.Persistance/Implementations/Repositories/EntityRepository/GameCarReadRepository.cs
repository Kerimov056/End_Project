﻿using EndProject.Application.Abstraction.Repositories.IEntityRepository;
using EndProject.Domain.Entitys;
using EndProjet.Persistance.Context;

namespace EndProjet.Persistance.Implementations.Repositories.EntityRepository;

public class GameCarReadRepository : ReadRepository<GameCar>, IGameCarReadRepository
{
    public GameCarReadRepository(AppDbContext context) : base(context)
    {
    }
}
