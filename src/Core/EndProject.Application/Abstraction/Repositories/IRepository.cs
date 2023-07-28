using EndProject.Domain.Entitys.Common;
using Microsoft.EntityFrameworkCore;

namespace EndProject.Application.Abstraction.Repositories;

public interface IRepository<T> where T : BaseEntity, new()
{
    public DbSet<T> Table { get;} 
}
