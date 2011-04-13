using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using MvcStarterProject.Business;
using MvcStarterProject.DataAccess;
using NUnit.Framework;

namespace MvcStarterProject.Tests
{
    [TestFixture]
    public class Class1
    {
        [Test]
        [Ignore]
        public void Test_adding_a_product()
        {
            var dc = new DataContext(ConfigurationManager.ConnectionStrings["MainDatabase"].ConnectionString);
            dc.Products.Add(new Product {Name = "tv", Price = 800});
            dc.SaveChanges();
        }
    }
}
