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
        private IOrderProcessor _orderProcessor;

        protected override void Establish_context()
        {
            var productRepository = ObjectFactory.GetInstance<ISaveObjectService<Product>>();
            var orderRepository = ObjectFactory.GetInstance<ISaveObjectService<Order>>();
 
            var product = new Product { Price = 10, IsActive = true, Name = "foo" };
            productRepository.Create(product);

            _order = new Order
                         {
                             StateCode = "OH",
                             Products = new List<Product> { product }
                         };
            orderRepository.Create(_order);
            _orderProcessor = ObjectFactory.GetInstance<IOrderProcessor>();
        }

        [Test]
        public void Should_calculate_the_subtotal_before_tax_and_shipping()
        {
            _orderProcessor.SubtotalBeforeTaxAndShipping(_order).ShouldEqual(10m);
        }

        [Test]
        public void Should_calculate_the_shipping_charges()
        {
            _orderProcessor.ShippingCharges(_order).ShouldEqual(5m);
        }

        [Test]
        public void Should_calculate_tax()
        {
            _orderProcessor.Tax(_order).ShouldEqual(0.7m);
        }

        [Test]
        public void Should_add_tax_and_shipping_to_the_order_total()
        {
            _orderProcessor.TotalPrice(_order).ShouldEqual(15.7m); // 10 + 5 (shipping) + 0.7 (tax)
        }
    }
}
