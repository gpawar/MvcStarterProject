using System.Configuration;
using System.Web.Mvc;
using MvcStarterProject.Business;
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
                                       scan.With(new RepositoryConventionScanner());
                                       scan.With(new GetObjectServiceConventionScanner());
                                       scan.With(new SaveObjectServiceConventionScanner());
                                       scan.With(new DeleteObjectServiceConventionScanner());
                                       scan.AssemblyContainingType(typeof(StructureMapBootstrapper));
                                   });
                        
                        x.For<IDataContext>()
                            .LifecycleIs(new UniquePerRequestLifecycle())
                            .Use(c =>
                                new DataContext(ConfigurationManager.ConnectionStrings["MainDatabase"].ConnectionString));
                        x.For(typeof(IRepository<>)).Use(typeof(Repository<>));
                        x.For(typeof(IGetObjectService<>)).Use(typeof(GetObjectService<>));
                        x.For(typeof(ISaveObjectService<>)).Use(typeof(SaveObjectService<>));
                        x.For(typeof(IDeleteObjectService<>)).Use(typeof(DeleteObjectService<>));
                    });
            DependencyResolver.SetResolver(new StructureMapDependencyResolver(ObjectFactory.Container));
        }
    }
}