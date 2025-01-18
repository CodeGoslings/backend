namespace HACS.Interfaces;

public interface IRepository<T>
{
    Task<List<T>> GetAllAsync();
    Task<T?> GetByIdAsync(Guid id);
    Task<T?> CreateAsync(T item, string? attribute = null);
    Task<T?> UpdateAsync(T item);
    Task DeleteAsync(Guid id);
}