using MvcStarterProject.DataAccess;

namespace MvcStarterProject.Business
{
    public class DeleteObjectService<T> : IDeleteObjectService<T> where T : class
    {
        private readonly IRepository<T> _repository;

        public DeleteObjectService(IRepository<T> repository)
        {
            _repository = repository;
        }

        public void Delete(T obj)
        {
            _repository.Delete(obj);
        }
    }
}