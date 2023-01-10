namespace Domain.RepositoryInterfaces;

public interface IRepository<in T> where T : class
{
    Task InsertAsync(T entity);
    Task UpdateAsync(T entity);
    Task DeleteAsync(T entity);
}
