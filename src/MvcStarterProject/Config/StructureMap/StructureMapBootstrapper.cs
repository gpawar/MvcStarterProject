using System.Configuration;
using System.Web.Mvc;
using MvcStarterProject.DataAccess;
using StructureMap;
using StructureMap.Pipeline;

namespace MvcStarterProject.Config.StructureMap
{
    public class StructureMapBootstrapper
    {
        public void Bootstrap()
        {
            IContainer container = new Container(
                x =>
                    {
                        x.For<IDataContext>()
                            .LifecycleIs(new UniquePerRequestLifecycle())
                            .Use(c =>
                                new DataContext(ConfigurationManager.ConnectionStrings["MainDatabase"].ConnectionString));
                    });
            DependencyResolver.SetResolver(new StructureMapDependencyResolver(container));
        }
    }
}