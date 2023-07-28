using EndProject.Application.Abstraction.Repositories;
using EndProject.Domain.Entitys.Common;
using Microsoft.EntityFrameworkCore;

namespace EndProjet.Persistance.Implementations;

public class WriteRepository<T> : IWriteRepository<T> where T : BaseEntity, new()
{
    public DbSet<T> Table => throw new NotImplementedException();

    public Task AddAsync(T entity)
    {
        throw new NotImplementedException();
    }

    public Task AddRangeAsync(IEnumerable<T> entities)
    {
        throw new NotImplementedException();
    }

    public void Remove(T entity)
    {
        throw new NotImplementedException();
    }

    public void RemoveRange(IEnumerable<T> entities)
    {
        throw new NotImplementedException();
    }

    public Task SavaChangeAsync()
    {
        throw new NotImplementedException();
    }

    public void Update(T entity)
    {
        throw new NotImplementedException();
    }
}
