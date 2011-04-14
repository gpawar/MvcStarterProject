using MvcStarterProject.DataAccess;

namespace MvcStarterProject.Config.StructureMap
{
    /// <summary>
    /// This TypeScanner looks for any concrete class that implements
    /// an IRepository<T> interface, and registers that type
    /// against the closed IRepository<T> interface,
    /// i.e. IRepository<Address> or IRepository<Site>
    /// </summary>
    public class RepositoryConventionScanner : CloseInterfaceConventionScanner
    {
        public RepositoryConventionScanner()
            : base(typeof(IRepository<>))
        {
        }
    }
}