using System.Collections.Generic;
using BankAccount.Tests;
using MvcStarterProject.Business;
using NUnit.Framework;
using Should;

namespace MvcStarterProject.Tests.UnitTests.Business
{
    public class When_calculating_the_total_price_of_all_products : Specification
    {
        private Order _order;

        protected override void Establish_context()
        {
            base.Establish_context();

            _order = new Order
                         {
                             Products = new List<Product>
                                            {
                                                new Product {Price = 1.23m},
                                                new Product {Price = 4.44m}
                                            }
                         };
        }

        [Test]
        public void Should_total_the_price_of_all_products_on_the_order()
        {
            _order.TotalPriceOfAllProducts.ShouldEqual(5.67m);
        }
    }
}