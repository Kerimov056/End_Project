using EndProject.Application.Abstraction.Repositories;
using EndProject.Domain.Entitys.Common;
using EndProjet.Persistance.Context;
using Microsoft.EntityFrameworkCore;

namespace EndProjet.Persistance.Implementations;

public class WriteRepository<T> : IWriteRepository<T> where T : BaseEntity, new()
{
    private readonly AppDbContext _context;
    public WriteRepository(AppDbContext context) => _context = context;
    public DbSet<T> Table => throw new NotImplementedException();

    public async Task AddAsync(T entity) => await Table.AddAsync(entity);

    public async Task AddRangeAsync(IEnumerable<T> entities) => await Table.AddRangeAsync(entities);

    public void Remove(T entity) => Table.Remove(entity);

    public void RemoveRange(IEnumerable<T> entities) => Table.RemoveRange(entities);
    public void Update(T entity) => Table.Update(entity);

    public async Task SavaChangeAsync() => await _context.SaveChangesAsync();

}
