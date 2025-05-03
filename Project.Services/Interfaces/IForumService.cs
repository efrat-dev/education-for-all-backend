namespace Project.Services.Interfaces
{
    public interface IForumService<T>
    {
        public Task<T> GetByIdAsync(int id);
        public Task<List<T>> GetAllAsync();
        public Task<T?> AddAsync(T item);
        public Task<T> UpdateAsync(T item);
        public Task DeleteByIdAsync(int id);
    }
}
