using MvcStarterProject.Business;

namespace MvcStarterProject.Config.StructureMap
{
    ///  <summary>/span>
    /// This TypeScanner looks for any concrete class that implements
    /// an IDeleteObjectService<T> interface, and registers that type
    /// against the closed IDeleteObjectService<T> interface,
    /// i.e. IDeleteObjectService<Address> or IDeleteObjectService<Site>
    /// </summary>
    public class DeleteObjectServiceConventionScanner : CloseInterfaceConventionScanner
    {
        public DeleteObjectServiceConventionScanner()
            : base(typeof(IDeleteObjectService<>))
        {
        }
    }
}