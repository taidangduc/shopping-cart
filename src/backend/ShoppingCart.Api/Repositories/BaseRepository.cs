using Microsoft.EntityFrameworkCore;
using ShoppingCart.Api.Data;

namespace ShoppingCart.Api.Repositories;

public class BaseRepository<T>(AppDbContext db) : IRepository<T> where T : class
{
    protected readonly AppDbContext Db = db;

    public async Task<T?> GetByIdAsync(string id, CancellationToken ct = default)
        => await Db.Set<T>().FindAsync([id], ct).AsTask();

    public async Task<(IEnumerable<T>, int Total)> GetAllAsync(int skip, int take, CancellationToken ct = default)
    {
        var total = await Db.Set<T>().CountAsync(ct);
        var items = await Db.Set<T>().Skip(skip).Take(take).ToListAsync(ct);
        return (items, total);
    }

    public async Task<T> AddAsync(T entity, CancellationToken ct = default)
    {
        var entry = await Db.Set<T>().AddAsync(entity, ct);
        return entry.Entity;
    }

    public T Update(T entity)
    {
        var entry = Db.Set<T>().Update(entity);
        return entry.Entity;
    }

    public void Delete(T entity) => Db.Set<T>().Remove(entity);
}