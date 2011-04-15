namespace MvcStarterProject.Business
{
    public interface ISaveObjectService<T> where T : class
    {
        void Create(T obj);
        void Update(T obj);
    }
}