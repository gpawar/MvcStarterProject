using MvcStarterProject.DataAccess;

namespace MvcStarterProject.Business
{
    public class SaveObjectService<T> : ISaveObjectService<T> where T : class
    {
        private readonly IRepository<T> _repository;

        public SaveObjectService(IRepository<T> repository)
        {
            _repository = repository;
        }

        public void Create(T obj)
        {
            _repository.Create(obj);
        }

        public void Update(T obj)
        {
            _repository.Update(obj);
        }
    }
}