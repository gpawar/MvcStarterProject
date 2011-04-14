using MvcStarterProject.Business;

namespace MvcStarterProject.Config.StructureMap
{
    ///  <summary>/span>
    /// This TypeScanner looks for any concrete class that implements
    /// an IGetObjectService<T> interface, and registers that type
    /// against the closed IGetObjectService<T> interface,
    /// i.e. IGetObjectService<Address> or IGetObjectService<Site>
    /// </summary>
    public class GetObjectServiceConventionScanner : CloseInterfaceConventionScanner
    {
        public GetObjectServiceConventionScanner() : base(typeof(IGetObjectService<>))
        {
        }
    }
}