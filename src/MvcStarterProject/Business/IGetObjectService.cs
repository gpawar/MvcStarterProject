using System.Collections.Generic;

namespace MvcStarterProject.Business
{
    public interface IGetObjectService<T> where T : class
    {
        T Get(int id);
        IList<T> GetAll();
    }
}