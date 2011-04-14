namespace MvcStarterProject.Business
{
    public interface ISaveObjectService<T> where T : class
    {
        void Save(T obj);
    }
}