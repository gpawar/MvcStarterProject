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
    public class When_viewing_an_order : Specification
    {
        private RhinoAutoMocker<OrderController> _mocker;
        private OrderIndexViewModel _model;
        private Product _product1;
        private IHttpSession _session;
        private Product _product2;

        protected override void Establish_context()
        {
            base.Establish_context();

            _product1 = new Product {ProductId = 1, Name = "one", Price = 1.11m};
            _product2 = new Product {ProductId = 2, Name = "two", Price = 2.22m};

            _mocker = new RhinoAutoMocker<OrderController>();
            _session = _mocker.Get<IHttpSession>();

            _mocker.Get<IGetProductService>().Stub(s => s.GetAvailableProducts())
                .Return(new[]
                            {
                                _product1,
                                _product2,
                                new Product {ProductId = 3, Name = "three", Price = 3.33m}
                            });

            AddProductToOrder(_product1);
            AddProductToOrder(_product2);
        }

        private void AddProductToOrder(Product product)
        {
            var productsInOrder = _session["ProductsInOrder"] as IList<Product>;
            if (productsInOrder == null)
            {
                productsInOrder = new List<Product>();
                _session["ProductsInOrder"] = productsInOrder;
            }

            productsInOrder.Add(product);
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
        public void Should_show_the_subtotal_before_tax()
        {
            
        }

        [Test]
        public void Should_show_the_shipping_charges()
        {
            
        }

        [Test]
        public void Should_show_the_tax()
        {
            
        }

        [Test]
        public void Should_show_the_total_for_the_entire_order()
        {
            
        }
    }
}