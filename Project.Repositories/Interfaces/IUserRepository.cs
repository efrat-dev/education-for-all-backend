namespace Project.Repositories.Interfaces
{
    public interface IUserRepository<T>
    {
        Task<T> AddAsync(T entity);
        Task DeleteByIdAsync(int id);
        Task<List<T>> GetAllUsersAsync();
        Task<T?> GetByIdAsync(int id);
        Task<T> UpdateAsync(T entity);
        Task<bool> EmailExistsAsync(string email);
        Task<bool> UserNameExistsAsync(string userName);
    }
}
