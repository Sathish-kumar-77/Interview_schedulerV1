using System.Linq.Expressions;

namespace Assignment.Contracts.Data.Repositories
{
    public interface IRepository<T>
    {
        IEnumerable<T> GetAll();
        T Get(object id);
        void Add(T entity);
        void Update(T entity);
        void Delete(object id);
        int Count();
        IEnumerable<T> Find(Expression<Func<T, bool>> predicate);
    }
}