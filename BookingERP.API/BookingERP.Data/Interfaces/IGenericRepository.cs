namespace BookingERP.Data.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        Task AddAsync(T entity);
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdLazy(Guid id);   
        Task DeleteAsync(Guid id);
        Task UpdateAsync(T entity);
    }
}
