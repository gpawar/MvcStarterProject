using System.Collections.Generic;
using MvcStarterProject.Business;

namespace MvcStarterProject.Tests.TestDataBuilders
{
    public class OrderBuilder
    {
        public static Order CreateOrderWithProductsForState(string state)
        {
            return new Order()
            {
                Products = new List<Product>
                                          {
                                              new Product {Price = 2m},
                                              new Product {Price = 2.5m}
                                          },
                StateCode = state
            };
        }
    }
}