using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Objects;
using System.Linq;
using System.Linq.Expressions;

namespace MvcStarterProject.DataAccess
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly IDataContext _dataContext;
        private readonly DbSet<T> _set;

        public Repository(IDataContext dataContext)
        {
            _dataContext = dataContext;
            _set = _dataContext.Set<T>();
        }

        public IQueryable<T> AsQueryable()
        {
            return _set;
        }

        public T Get(int id)
        {
            return _set.Find(id);
        }

        public IEnumerable<T> GetAll()
        {
            return _set.ToList();
        }

        public IEnumerable<T> Find(Expression<Func<T, bool>> where)
        {
            return _set.Where(where);
        }

        public T Single(Expression<Func<T, bool>> where)
        {
            return _set.Single(where);
        }

        public T First(Expression<Func<T, bool>> where)
        {
            return _set.First(where);
        }

        public void Delete(T entity)
        {
            _set.Remove(entity);
            _dataContext.SaveChanges();
        }

        public void Save(T entity)
        {
            if (_set.Contains(entity))
                _set.Attach(entity);
            else
                _set.Add(entity);
            _dataContext.SaveChanges();
        }
    }
}