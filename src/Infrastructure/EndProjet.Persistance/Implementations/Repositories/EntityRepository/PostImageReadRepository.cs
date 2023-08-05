﻿using EndProject.Application.Abstraction.Repositories.IEntityRepository;
using EndProject.Domain.Entitys;
using EndProjet.Persistance.Context;

namespace EndProjet.Persistance.Implementations.Repositories.EntityRepository;

public class PostImageReadRepository : ReadRepository<PostImage>, IPostImageReadRepository
{
    public PostImageReadRepository(AppDbContext context) : base(context)
    {
    }
}