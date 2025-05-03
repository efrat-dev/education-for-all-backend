namespace Project.Repositories.Interfaces
{
    public interface IForumRepository<T>
    {
        Task<T> AddAsync(T entity);
        Task DeleteByIdAsync(int id);
        Task<List<T>> GetAllAsync();
        Task<T?> GetByIdAsync(int id);
        Task<T> UpdateAsync(T entity);
    }
}
