namespace AutoSchedule.DAL.Interface;

public interface IBaseRepository<T>
{
    Task<bool> Create(T entity);
    IQueryable<T> GetAll();
    Task<T> GetById(int id);
    Task<IEnumerable<T>> GetByPage(int pageNumber, int pageSize);
    Task<int> GetCount();
    IQueryable<T> Search(string searchTerm);
    Task<T> Update(T entity);
    Task<bool> Delete(T entity);
}
