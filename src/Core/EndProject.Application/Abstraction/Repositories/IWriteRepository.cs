using EndProject.Domain.Entitys.Common;

namespace EndProject.Application.Abstraction.Repositories;

public interface IWriteRepository<T>:IRepository<T> where T : BaseEntity, new()
{
    Task AddAsync(T entity);    
    Task AddRangeAsync(IEnumerable<T> entities);
    void Remove(T entity);
    void RemoveRange(IEnumerable<T> entities);
    void Update(T entity);
    Task SavaChangeAsync();
}
