using AngularProject.Data;
using AngularProject.Models;
using System.Linq.Expressions;

namespace AngularAPI.Services
{
    public class GenericRepositoryT<T> : IGenericRepository<T> where T : class
    {
        private readonly ShoppingDbContext context;
        public GenericRepositoryT(ShoppingDbContext _context) { context = _context;  }

        public virtual T Add(T entity)
        {
            T res = context.Set<T>().Add(entity).Entity;
            context.SaveChanges();
            return res;
        }

        public virtual List<T> FindMany(Expression<Func<T, bool>> predicate)
        {
            return context.Set<T>().Where(predicate).ToList();
        }

        public virtual T? FindOne(Expression<Func<T, bool>> exp)
        {
            return context.Set<T>().FirstOrDefault(exp);
        }

        public virtual List<T> GetAll()
        {
            return context.Set<T>().ToList();
        }

        public virtual T? GetById(int id)
        {
            return context.Set<T>().Find(id);
        }

        public virtual T? GetById(string id)
        {
            return context.Set<T>().Find(id);
        }

        public virtual T Remove(T entity)
        {
            T res = context.Set<T>().Remove(entity).Entity;
            context.SaveChanges();
            return res;
        }

        public virtual T Update(T entity)
        {
            T res = context.Set<T>().Update(entity).Entity;
            context.SaveChanges();
            return res;
        }
    }
}
