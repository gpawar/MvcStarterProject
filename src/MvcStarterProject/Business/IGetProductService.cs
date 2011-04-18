using System;
using System.Collections.Generic;

namespace MvcStarterProject.Business
{
    public interface IGetProductService : IGetObjectService<Product>
    {
        IList<Product> GetAvailableProducts();
    }
}