using System;
using StructureMap.Configuration.DSL;
using StructureMap.Graph;
using StructureMap.TypeRules;

namespace MvcStarterProject.Config.StructureMap
{
    ///  <summary>/span>
    /// This TypeScanner looks for any concrete class that implements
    /// an IGetObjectService<T> interface, and registers that type
    /// against the closed IGetObjectService<T> interface,
    /// i.e. IGetObjectService<Address> or IGetObjectService<Site>
    /// </summary>
    public abstract class CloseInterfaceConventionScanner : IRegistrationConvention
    {
        private readonly Type _openGenericType;

        protected CloseInterfaceConventionScanner(Type openGenericType)
        {
            _openGenericType = openGenericType;
        }

        public void Process(Type type, Registry registry)
        {
            Type interfaceType = type.FindInterfaceThatCloses(_openGenericType);
            if (interfaceType != null)
            {
                registry.AddType(interfaceType, type);
            }
        }
    }
}