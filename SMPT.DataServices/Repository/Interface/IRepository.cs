using System.Linq.Expressions;

namespace SMPT.DataServices.Repository.Interface
{
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAll();
        Task<T?> GetById(Guid id);
        Task<bool> Add(T entity);
        Task<bool> Update(T entity);
        Task<bool> Delete(Guid id);

        Task<IEnumerable<T>> FindAll(Expression<Func<T, bool>>? filter = null);
        Task<T?> Find(Expression<Func<T, bool>>? filter = null, bool tracked = true);
    }
}
