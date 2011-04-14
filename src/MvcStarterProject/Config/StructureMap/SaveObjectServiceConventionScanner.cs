using MvcStarterProject.Business;

namespace MvcStarterProject.Config.StructureMap
{
    ///  <summary>/span>
    /// This TypeScanner looks for any concrete class that implements
    /// an ISaveObjectService<T> interface, and registers that type
    /// against the closed ISaveObjectService<T> interface,
    /// i.e. ISaveObjectService<Address> or ISaveObjectService<Site>
    /// </summary>
    public class SaveObjectServiceConventionScanner : CloseInterfaceConventionScanner
    {
        public SaveObjectServiceConventionScanner()
            : base(typeof(ISaveObjectService<>))
        {
        }
    }
}