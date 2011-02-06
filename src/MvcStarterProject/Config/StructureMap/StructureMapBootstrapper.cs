using System.Web.Mvc;
using StructureMap;

namespace MvcStarterProject.Config.StructureMap
{
    public class StructureMapBootstrapper
    {
        public void Bootstrap()
        {
            IContainer container = new Container(
                x =>
                    {

                    });
            DependencyResolver.SetResolver(new StructureMapDependencyResolver(container));
        }
    }
}