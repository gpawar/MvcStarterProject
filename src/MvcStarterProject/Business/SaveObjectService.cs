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

        public void Save(T obj)
        {
            _repository.Save(obj);
        }
    }
}