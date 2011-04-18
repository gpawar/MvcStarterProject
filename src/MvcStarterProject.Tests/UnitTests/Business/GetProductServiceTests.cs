using System.Linq;
using BankAccount.Tests;
using MvcStarterProject.Business;
using MvcStarterProject.DataAccess;
using System.Collections.Generic;
using NUnit.Framework;
using Rhino.Mocks;
using Should;
using StructureMap.AutoMocking;

namespace MvcStarterProject.Tests.UnitTests.Business
{
    public class When_getting_the_list_of_available_products : Specification
    {
        private RhinoAutoMocker<GetProductService> _mocker;
        private IList<Product> _availableProducts;

        protected override void Establish_context()
        {
            base.Establish_context();

            _mocker = new RhinoAutoMocker<GetProductService>();
            var repository = _mocker.Get<IRepository<Product>>();
            repository.Stub(r => r.AsQueryable())
                .Return(new List<Product>
                            {
                                new Product {ProductId = 1, IsActive = true, Name = "one"},
                                new Product {ProductId = 2, IsActive = true, Name = "two"},
                                new Product {ProductId = 3, IsActive = false, Name = "three"}
                            }.AsQueryable());
        }

        protected override void Because_of()
        {
            _availableProducts = _mocker.ClassUnderTest.GetAvailableProducts();
        }

        [Test]
        public void Should_return_active_products()
        {
            _availableProducts.Count.ShouldEqual(2);

            _availableProducts.Single(p => p.ProductId == 1).ShouldNotBeNull();
            _availableProducts.Single(p => p.ProductId == 2).ShouldNotBeNull();
        }
    }
}