using EndProject.Application.Abstraction.Repositories;
using EndProject.Domain.Entitys.Common;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace EndProjet.Persistance.Implementations;

public class ReadRepository<T> : IReadRepository<T> where T : BaseEntity, new()
{
    public DbSet<T> Table => throw new NotImplementedException();

    public IQueryable<T> GetAll(bool IsTracking = true, params string[] inculdes)
    {
        throw new NotImplementedException();
    }

    public IQueryable<T> GetAllExpression(Expression<Func<T, bool>> expression, int Skip, int Take, bool IsTracking = true, params string[] inculdes)
    {
        throw new NotImplementedException();
    }

    public IQueryable<T> GetAllExpressionOrderBy(Expression<Func<T, bool>> expression, int Skip, int Take, Expression<Func<T, object>> expressionOrder, bool IsOrder = true, bool IsTracking = true, params string[] inculdes)
    {
        throw new NotImplementedException();
    }

    public Task<T> GetByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<T?> GetByIdAsyncExpression(Expression<Func<T, bool>> expression, bool isTracking = true)
    {
        throw new NotImplementedException();
    }
}
