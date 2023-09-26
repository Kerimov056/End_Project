﻿using EndProject.Domain.Entitys;

namespace EndProject.Application.Abstraction.Repositories.IEntityRepository;

public interface ITripeReadRepository : IReadRepository<Trip>
{
    Task<int> MyTripCount(string appUserId);
}
