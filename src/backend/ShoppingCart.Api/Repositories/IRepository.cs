namespace ShoppingCart.Api.Repositories;

public interface IRepository<T> where T : class
{
    Task<T?> GetByIdAsync(string id, CancellationToken ct = default);
    Task<(IEnumerable<T>, int Total)> GetAllAsync(int skip, int take, CancellationToken ct = default);
    Task<T> AddAsync(T entity, CancellationToken ct = default);
    T Update(T entity);
    void Delete(T entity);
}