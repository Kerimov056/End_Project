﻿using EndProject.Application.Abstraction.Repositories.IEntityRepository;
using EndProject.Domain.Entitys;
using EndProjet.Persistance.Context;

namespace EndProjet.Persistance.Implementations.Repositories.EntityRepository;

public class CarTagReadRepository : ReadRepository<CarTag>, ICarTagReadRepository
{
    public CarTagReadRepository(AppDbContext context) : base(context)
    {
    }
}
