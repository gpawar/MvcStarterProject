using MvcStarterProject.DataAccess;

namespace MvcStarterProject.Business
{
    public class OrderProcessor : IOrderProcessor
    {
        private readonly IDataContext _dataContext;
        private readonly ITaxCalculator _taxCalculator;
        private readonly IShippingCalculator _shippingCalculator;

        public OrderProcessor(IDataContext dataContext, ITaxCalculator taxCalculator, IShippingCalculator shippingCalculator)
        {
            _dataContext = dataContext;
            _taxCalculator = taxCalculator;
            _shippingCalculator = shippingCalculator;
        }

        public decimal CalculateTotalPrice(int orderId)
        {
            // load order from database
            var order = _dataContext.Orders.Find(orderId);
            var totalPriceOfAllProducts = order.TotalPriceOfAllProducts;

            // calculate tax
            decimal tax = _taxCalculator.CalculateTax(order);

            // calculate shipping
            decimal shippingCharges = _shippingCalculator.CalculateShipping(order);

            return totalPriceOfAllProducts + tax + shippingCharges;
        }
    }
}
