using System.Collections.Generic;
using MvcStarterProject.Business;
using MvcStarterProject.DataAccess;
using NUnit.Framework;
using Should;
using StructureMap;

namespace MvcStarterProject.Tests.IntegrationTests.Business
{
    public class When_calculating_the_total_price_of_an_order : IntegrationTest
    {
        private decimal _totalPrice;
        private Order _order;

        protected override void Establish_context()
        {
            var productRepository = ObjectFactory.GetInstance<IRepository<Product>>();
            var orderRepository = ObjectFactory.GetInstance<IRepository<Order>>();
 
            var product = new Product { Price = 10, IsActive = true, Name = "foo" };
            productRepository.Create(product);

            _order = new Order
                         {
                             StateCode = "OH",
                             Products = new List<Product> { product }
                         };
            orderRepository.Create(_order);
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
