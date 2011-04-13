namespace MvcStarterProject.Business
{
    public class ShippingCalculator : IShippingCalculator
    {
        public decimal CalculateShipping(Order order)
        {
            if (order.TotalPriceOfAllProducts < 25)
                return 5;
            return 0;
        }
    }
}