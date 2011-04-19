using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using BankAccount.Tests;
using HttpInterfaces;
using MvcStarterProject.Business;
using MvcStarterProject.Controllers;
using MvcStarterProject.Models;
using NUnit.Framework;
using Rhino.Mocks;
using Should;
using StructureMap.AutoMocking;

namespace MvcStarterProject.Tests.UnitTests.Controllers
{
    public abstract class Given_an_OrderController : Specification
    {
        protected RhinoAutoMocker<OrderController> _mocker;
        protected Product _product1;
        protected Product _product2;
        protected IHttpSession _session;

        protected override void Establish_context()
        {
            base.Establish_context();

            _product1 = new Product {ProductId = 1, Name = "one", Price = 1.11m};
            _product2 = new Product {ProductId = 2, Name = "two", Price = 2.22m};

            _mocker = new RhinoAutoMocker<OrderController>();
     
            // stub the Session
            _session = new FakeHttpSession();
            _mocker.Inject<IHttpSession>(_session);

            // available products
            _mocker.Get<IGetProductService>().Stub(s => s.GetAvailableProducts())
                .Return(new[]
                            {
                                _product1,
                                _product2,
                                new Product {ProductId = 3, Name = "three", Price = 3.33m}
                            });
        }
    }

    public class When_viewing_the_order_page_and_an_order_has_not_been_started_yet : Given_an_OrderController
    {
        private OrderIndexViewModel _model;

        protected override void Because_of()
        {
            _model = (OrderIndexViewModel) ((ViewResult) _mocker.ClassUnderTest.Index()).ViewData.Model;
        }

        [Test]
        public void Should_show_a_list_of_all_available_products()
        {
            _model.AvailableProducts.Count.ShouldEqual(3);

            var product1 = _model.AvailableProducts.Single(p => p.ProductId == 1);
            product1.Name.ShouldEqual("one");
            product1.Price.ShouldEqual(1.11m);

            var product2 = _model.AvailableProducts.Single(p => p.ProductId == 2);
            product2.Name.ShouldEqual("two");
            product2.Price.ShouldEqual(2.22m);

            var product3 = _model.AvailableProducts.Single(p => p.ProductId == 3);
            product3.Name.ShouldEqual("three");
            product3.Price.ShouldEqual(3.33m);
        }

        [Test]
        public void Should_show_no_products_in_the_cart()
        {
            _model.ProductsInOrder.Count.ShouldEqual(0);
        }

        [Test]
        public void Should_show_the_subtotal_before_tax_and_shipping()
        {
            _model.SubtotalBeforeTaxAndShipping.ShouldEqual(0m);
        }

        [Test]
        public void Should_show_the_shipping_charges()
        {
            _model.ShippingCharges.ShouldEqual(0m);
        }

        [Test]
        public void Should_show_the_tax()
        {
            _model.Tax.ShouldEqual(0m);
        }

        [Test]
        public void Should_show_the_total_for_the_entire_order()
        {
            _model.TotalPrice.ShouldEqual(0m);   
        }
    }

    public class When_viewing_an_order_that_has_already_been_started : Given_an_OrderController
    {
        private OrderIndexViewModel _model;
        protected override void Establish_context()
        {
            base.Establish_context();

            // order
            var order = new Order
                            {
                                OrderId = 111,
                                Products = new List<Product> {_product1, _product2},
                                StateCode = "OH"
                            };
            _mocker.Get<IGetObjectService<Order>>().Stub(s => s.Get(111)).Return(order);
            _session["OrderId"] = 111;

            // order processor - we've already tested that the order
            // processor works, so we don't need to do it again
            var orderProcessor = _mocker.Get<IOrderProcessor>();
            orderProcessor.Stub(op => op.SubtotalBeforeTaxAndShipping(order)).Return(3.33m);
            orderProcessor.Stub(op => op.ShippingCharges(order)).Return(5);
            orderProcessor.Stub(op => op.Tax(order)).Return(0.23m);
            orderProcessor.Stub(op => op.TotalPrice(order)).Return(8.56m);
        }

        protected override void Because_of()
        {
            _model = (OrderIndexViewModel) ((ViewResult) _mocker.ClassUnderTest.Index()).ViewData.Model;
        }

        [Test]
        public void Should_show_a_list_of_all_available_products()
        {
            _model.AvailableProducts.Count.ShouldEqual(3);

            var product1 = _model.AvailableProducts.Single(p => p.ProductId == 1);
            product1.Name.ShouldEqual("one");
            product1.Price.ShouldEqual(1.11m);

            var product2 = _model.AvailableProducts.Single(p => p.ProductId == 2);
            product2.Name.ShouldEqual("two");
            product2.Price.ShouldEqual(2.22m);

            var product3 = _model.AvailableProducts.Single(p => p.ProductId == 3);
            product3.Name.ShouldEqual("three");
            product3.Price.ShouldEqual(3.33m);
        }

        [Test]
        public void Should_show_products_that_are_already_in_the_cart()
        {
            _model.ProductsInOrder.Count.ShouldEqual(2);

            var product1 = _model.AvailableProducts.Single(p => p.ProductId == 1);
            product1.Name.ShouldEqual("one");
            product1.Price.ShouldEqual(1.11m);

            var product2 = _model.AvailableProducts.Single(p => p.ProductId == 2);
            product2.Name.ShouldEqual("two");
            product2.Price.ShouldEqual(2.22m);
        }

        [Test]
        public void Should_show_the_subtotal_before_tax_and_shipping()
        {
            _model.SubtotalBeforeTaxAndShipping.ShouldEqual(3.33m);
        }

        [Test]
        public void Should_show_the_shipping_charges()
        {
            _model.ShippingCharges.ShouldEqual(5m);
        }

        [Test]
        public void Should_show_the_tax()
        {
            _model.Tax.ShouldEqual(0.23m);
        }

        [Test]
        public void Should_show_the_total_for_the_entire_order()
        {
            _model.TotalPrice.ShouldEqual(8.56m);   
        }
    }

    public class When_adding_a_product_to_an_order_that_has_already_been_started : Given_an_OrderController
    {
        private Order _order;

        protected override void Establish_context()
        {
            base.Establish_context();

            // order
            _order = new Order
                         {
                             OrderId = 111,
                             StateCode = "OH"
                         };
            _mocker.Get<IGetObjectService<Order>>().Stub(s => s.Get(111)).Return(_order);
            _session["OrderId"] = 111;

            _mocker.Get<IGetProductService>().Stub(s => s.Get(3)).Return(new Product {ProductId = 3});
        }

        protected override void Because_of()
        {
            _mocker.ClassUnderTest.AddToOrder(3);
        }

        [Test]
        public void Should_add_the_selected_product_to_the_order()
        {
            _order.Products.Single().ProductId.ShouldEqual(3);
        }

        [Test]
        public void Should_save_the_order()
        {
            _mocker.Get<ISaveObjectService<Order>>().AssertWasCalled(s => s.Update(_order));
        }
    }

    public class When_adding_a_product_to_a_new_order : Given_an_OrderController
    {
        protected override void Establish_context()
        {
            base.Establish_context();

            _mocker.Get<IGetProductService>().Stub(s => s.Get(3)).Return(new Product {ProductId = 3});
        }

        protected override void Because_of()
        {
            _mocker.ClassUnderTest.AddToOrder(3);
        }

        [Test]
        public void Should_add_the_selected_product_to_the_order()
        {
            var order = (Order) _mocker.Get<ISaveObjectService<Order>>().GetArgumentsForCallsMadeOn(s => s.Create(null))[0][0];
            order.Products.Single().ProductId.ShouldEqual(3);
        }

        [Test]
        public void Should_save_the_order_ID_in_session()
        {
            var order = (Order) _mocker.Get<ISaveObjectService<Order>>().GetArgumentsForCallsMadeOn(s => s.Create(null))[0][0];
            ((int)_mocker.Get<IHttpSession>()["OrderId"]).ShouldEqual(order.OrderId);
        }

        [Test]
        public void Should_save_the_order()
        {
            _mocker.Get<ISaveObjectService<Order>>().AssertWasCalled(s => s.Create(Arg<Order>.Is.Anything));
        }
    }
}