using System.Collections.Generic;
using MvcStarterProject.DataAccess;

namespace MvcStarterProject.Business
{
    public class GetObjectService<T> : IGetObjectService<T> where T : class
    {
        private readonly IRepository<T> _repository;

        public GetObjectService(IRepository<T> repository)
        {
            _repository = repository;
        }

        public T Get(int id)
        {
            return _repository.Get(id);
        }

        public IList<T> GetAll()
        {
            return _repository.GetAll();
        }
    }
}