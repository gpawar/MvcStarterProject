using System.Collections.Generic;
using BankAccount.Tests;
using MvcStarterProject.Business;
using NUnit.Framework;
using Should;

namespace MvcStarterProject.Tests.UnitTests.Business
{
    [TestFixture]
    public class When_calculating_the_shipping_for_orders_where_price_of_all_products_is_less_than_25 : Specification
    {
        [Test]
        public void Should_return_5()
        {
            foreach (var productPrice in new decimal[] { 1, 5, 10, 24.99m })
            {
                var order = new Order()
                                {
                                    Products = new List<Product>
                                                   {
                                                       new Product() {Price = productPrice}
                                                   }
                                };
                new ShippingCalculator().CalculateShipping(order).ShouldEqual(5m);
            }
        }
    }

    [TestFixture]
    public class When_calculating_the_shipping_for_orders_where_price_of_all_products_is_25_or_higher : Specification
    {
        [Test]
        public void Should_return_0()
        {
            foreach (var productPrice in new decimal[] { 25, 100, 10000 })
            {
                var order = new Order()
                                {
                                    Products = new List<Product>
                                                   {
                                                       new Product() {Price = productPrice}
                                                   }
                                };
                new ShippingCalculator().CalculateShipping(order).ShouldEqual(0);
            }
        }
    }
}