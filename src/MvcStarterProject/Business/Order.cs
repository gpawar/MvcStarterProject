using System.Collections.Generic;
using System.Linq;

namespace MvcStarterProject.Business
{
    public class Order
    {
        public int OrderId { get; set; }
        public virtual IList<Product> Products { get; set; }
        public string StateCode { get; set; }

        public decimal TotalPriceOfAllProducts
        {
            get { return Products.Sum(p => p.Price); }
        }

        public Order()
        {
            Products = new List<Product>();
        }
    }
}