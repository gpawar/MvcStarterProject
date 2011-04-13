using System.Collections.Generic;
using MvcStarterProject.Business;
using MvcStarterProject.DataAccess;
using MvcStarterProject.Tests.IntegrationTests;
using NUnit.Framework;
using Should;
using StructureMap;

namespace MvcStarterProject.Tests.UnitTests.Business
{
    public class When_calculating_the_total_price_of_an_order : IntegrationTest
    {
        private decimal _totalPrice;
        private Order _order;

        protected override void Establish_context()
        {
            var dataContext = ObjectFactory.GetInstance<IDataContext>();
 
            var product = new Product { Price = 10, IsActive = true, Name = "foo" };
            dataContext.Products.Add(product);

            _order = new Order
                         {
                             StateCode = "OH",
                             Products = new List<Product> { product }
                         };

            dataContext.Orders.Add(_order);
            dataContext.SaveChanges();
        }

        protected override void Because_of()
        {
            _totalPrice = ObjectFactory.GetInstance<IOrderProcessor>().CalculateTotalPrice(_order.OrderId);
        }

        [Test]
        public void Should_add_tax_and_shipping_to_the_order_total()
        {
            _totalPrice.ShouldEqual(15.7m); // 10 + 5 (shipping) + 0.7 (tax)
        }
    }
}
