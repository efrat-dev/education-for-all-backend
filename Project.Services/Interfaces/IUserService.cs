namespace Project.Services.Interfaces
{
    public interface IUserService<T>
    {
        public Task<List<T>> GetAllAsync();
        public Task<T> GetByIdAsync(int id);
        public Task<T?> AddAsync(T entity);
        public Task<T> UpdateAsync(T entity);
        public Task DeleteByIdAsync(int id);
        Task<bool> EmailExists(string email);
        Task<bool> UserNameExists(string userName);
    }
}
