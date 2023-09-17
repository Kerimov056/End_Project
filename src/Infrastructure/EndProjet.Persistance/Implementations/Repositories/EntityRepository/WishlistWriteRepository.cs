﻿using EndProject.Application.Abstraction.Repositories.IEntityRepository;
using EndProject.Domain.Entitys;
using EndProjet.Persistance.Context;

namespace EndProjet.Persistance.Implementations.Repositories.EntityRepository;

public class WishlistWriteRepository : WriteRepository<Wishlist>, IWishlistWriteRepository
{
    public WishlistWriteRepository(AppDbContext context) : base(context)
    {
    }
}
