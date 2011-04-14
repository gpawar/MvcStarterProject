namespace MvcStarterProject.Business
{
    public interface IDeleteObjectService<T> where T : class
    {
        void Delete(T obj);
    }
}