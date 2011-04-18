using System.Collections.Generic;
using MvcStarterProject.Business;

namespace MvcStarterProject.Models
{
    public class OrderIndexViewModel
    {
        public IList<Product> AvailableProducts { get; set; }
        public IList<Product> ProductsInOrder { get; set; }
        public decimal SubtotalBeforeTaxAndShipping { get; set; }
        public decimal Tax { get; set; }
        public decimal ShippingCharges { get; set; }
        public decimal TotalPrice { get; set; }
    }
}