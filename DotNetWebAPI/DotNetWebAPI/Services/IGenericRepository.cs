using System.Linq.Expressions;

namespace AngularAPI.Services
{
    public interface IGenericRepository<T> where T : class
    {

        public List<T> GetAll();
        public T? GetById(int id);
        public T? GetById(string id);
        public T Add(T entity);
        public T? Remove(T entity);
        public T? Update(T entity);
        public T? FindOne(Expression<Func<T, bool>> predicate);
        public List<T> FindMany(Expression<Func<T, bool>> predicate);
    }
}
