using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace MvcStarterProject.DataAccess
{
    public interface IRepository<T> where T : class
    {
        IQueryable<T> AsQueryable();
        T Get(int id);
        IEnumerable<T> GetAll();
        IEnumerable<T> Find(Expression<Func<T, bool>> where);
        T Single(Expression<Func<T, bool>> where);
        T First(Expression<Func<T, bool>> where);
        void Delete(T entity);
        void Save(T entity);
    }
}