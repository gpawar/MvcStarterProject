using System;

namespace MvcStarterProject.Business
{
    public class TaxCalculator : ITaxCalculator
    {
        public decimal CalculateTax(Order order)
        {
            if (order.StateCode == "OH")
                return Math.Round(order.TotalPriceOfAllProducts * .07m, 2);
            else if (order.StateCode == "MI")
                return Math.Round(order.TotalPriceOfAllProducts * .065m, 2);
            return 0;
        }
    }
}