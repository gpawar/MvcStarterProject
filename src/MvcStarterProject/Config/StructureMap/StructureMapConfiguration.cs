using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using HttpInterfaces;
using MvcStarterProject.Business;
using MvcStarterProject.DataAccess;
using StructureMap;
using StructureMap.Pipeline;

namespace MvcStarterProject.Config.StructureMap
{
    public class StructureMapConfiguration
    {
        private IList<Assembly> _assembliesToScan = new List<Assembly>();

        public void ScanAssembly(Assembly assembly)
        {
            _assembliesToScan.Add(assembly);
        }

        public void Configure()
        {
            ObjectFactory.Configure(x =>
                                        {
                                            x.Scan(scan =>
                                                       {
                                                           scan.WithDefaultConventions();
                                                           scan.ConnectImplementationsToTypesClosing(typeof(IRepository<>));
                                                           scan.ConnectImplementationsToTypesClosing(typeof(IGetObjectService<>));
                                                           scan.ConnectImplementationsToTypesClosing(typeof(ISaveObjectService<>));
                                                           scan.ConnectImplementationsToTypesClosing(typeof(IDeleteObjectService<>));
                                                           scan.AssemblyContainingType(typeof(StructureMapConfiguration));

                                                           foreach (var assembly in _assembliesToScan)
                                                               scan.Assembly(assembly);
                                                       });
                        
                                            x.For<IDataContext>()
                                                .LifecycleIs(new HybridLifecycle())
                                                .Use(c =>
                                                     new DataContext(ConfigurationManager.ConnectionStrings["MainDatabase"].ConnectionString));
                                            x.For(typeof(IRepository<>)).Use(typeof(Repository<>));
                                            x.For(typeof(IGetObjectService<>)).Use(typeof(GetObjectService<>));
                                            x.For(typeof(ISaveObjectService<>)).Use(typeof(SaveObjectService<>));
                                            x.For(typeof(IDeleteObjectService<>)).Use(typeof(DeleteObjectService<>));

                                            ConfigureHttpInterfaces(x);
                                        });
            DependencyResolver.SetResolver(new StructureMapDependencyResolver(ObjectFactory.Container));
        }

        public void Reset()
        {
            ObjectFactory.Initialize(x => { });
        }

        private void ConfigureHttpInterfaces(ConfigurationExpression x)
        {
            // Woot!!  Interfaces for HttpContext stuff!  Goodbye to nasty unmockable
            // abstract base classes.
            // http://haacked.com/archive/2007/09/09/ihttpcontext-and-other-interfaces-for-your-duck-typing-benefit.aspx
            // Update: the third party library in that post causes problems in production if multiple
            // requests during the bootstrapping process.  I made hand-rolled proxies instead, which you can get here:
            // http://github.com/jonkruger/httpinterfaces
            x.For<IHttpApplication>().Use(
                c => WebContext.Cast(HttpContext.Current.ApplicationInstance));
            x.For<IHttpApplicationState>().Use(
                c => WebContext.Cast(HttpContext.Current.Application));
            x.For<IHttpCachePolicy>().Use(
                c => WebContext.Cast(HttpContext.Current.Response.Cache));
            x.For<IHttpClientCertificate>().Use(
                c => WebContext.Cast(HttpContext.Current.Request.ClientCertificate));
            x.For<IHttpContext>().Use(
                c => WebContext.Cast(HttpContext.Current));
            x.For<IHttpFileCollection>().Use(
                c => WebContext.Cast(HttpContext.Current.Request.Files));
            x.For<IHttpModuleCollection>().Use(
                c => WebContext.Cast(HttpContext.Current.ApplicationInstance.Modules));
            x.For<IHttpRequest>().Use(
                c => WebContext.Cast(HttpContext.Current.Request));
            x.For<IHttpResponse>().Use(
                c => WebContext.Cast(HttpContext.Current.Response));
            x.For<IHttpServerUtility>().Use(
                c => WebContext.Cast(HttpContext.Current.Server));
            x.For<IHttpSession>().Use(
                c => WebContext.Cast(HttpContext.Current.Session));
            x.For<ITraceContext>().Use(
                c => WebContext.Cast(HttpContext.Current.Trace));

        }
    }
}