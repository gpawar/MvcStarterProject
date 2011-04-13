using System.Configuration;
using System.Web.Mvc;
using MvcStarterProject.DataAccess;
using StructureMap;
using StructureMap.Pipeline;

namespace MvcStarterProject.Config.StructureMap
{
    public class StructureMapBootstrapper
    {
        public static bool AlreadyStarted { get; private set;}

        public static void Bootstrap()
        {
            if (AlreadyStarted)
                return;

            AlreadyStarted = true;
            ObjectFactory.Configure(x =>
                    {
                        x.Scan(scan =>
                                   {
                                       scan.WithDefaultConventions();
                                       scan.AssemblyContainingType(typeof(StructureMapBootstrapper));
                                   });
                        
                        x.For<IDataContext>()
                            .LifecycleIs(new UniquePerRequestLifecycle())
                            .Use(c =>
                                new DataContext(ConfigurationManager.ConnectionStrings["MainDatabase"].ConnectionString));
                    });
            DependencyResolver.SetResolver(new StructureMapDependencyResolver(ObjectFactory.Container));
        }
    }
}