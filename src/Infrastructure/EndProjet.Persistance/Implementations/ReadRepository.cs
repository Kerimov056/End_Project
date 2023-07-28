using EndProject.Application.Abstraction.Repositories;
using EndProject.Domain.Entitys.Common;
using EndProjet.Persistance.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace EndProjet.Persistance.Implementations;

public class ReadRepository<T> : IReadRepository<T> where T : BaseEntity, new()
{
    private readonly AppDbContext _context;
    public ReadRepository(AppDbContext context) => _context = context;
    public DbSet<T> Table => _context.Set<T>();

    public IQueryable<T> GetAll(bool IsTracking = true, params string[] inculdes)
    {
        var query = Table.AsQueryable();
        foreach (var inculde in inculdes)
        {
            query = query.Include(inculde);
        }
        return IsTracking ? query : query.AsNoTracking();
    }

    public IQueryable<T> GetAllExpression(Expression<Func<T, bool>> expression, int Skip, int Take, bool IsTracking = true, params string[] inculdes)
    {
       var query = Table.Where(expression).Skip(Skip).Take(Take).AsQueryable();
        foreach (var inculde in inculdes)
        {
            query = query.Include(inculde);
        }
        return IsTracking ? query : query.AsNoTracking();
    }

    public IQueryable<T> GetAllExpressionOrderBy(Expression<Func<T, bool>> expression, int Skip, int Take, Expression<Func<T, object>> expressionOrder, bool IsOrder = true, bool IsTracking = true, params string[] inculdes)
    {
        var query = Table.Where(expression).AsQueryable();
        query = IsOrder ? query.OrderBy(expressionOrder) : query.OrderByDescending(expression);
        foreach (var inculude in inculdes)
        {
            query = query.Include(inculude);
        }
        return IsTracking ? query : query.AsNoTracking();
    }

    public async Task<T> GetByIdAsync(Guid id)
    {
        var query = await Table.FindAsync(id);
        return query;
    }

    public async Task<T?> GetByIdAsyncExpression(Expression<Func<T, bool>> expression, bool isTracking = true)
    {
        var query = isTracking ? Table.AsQueryable() : Table.AsNoTracking();
        return await query.FirstOrDefaultAsync(expression);
    }
}
