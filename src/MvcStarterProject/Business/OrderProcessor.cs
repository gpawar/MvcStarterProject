using MvcStarterProject.DataAccess;

namespace MvcStarterProject.Business
{
    public class OrderProcessor : IOrderProcessor
    {
        private readonly IGetObjectService<Order> _getOrderService;
        private readonly ITaxCalculator _taxCalculator;
        private readonly IShippingCalculator _shippingCalculator;

        public OrderProcessor(IGetObjectService<Order> getOrderService, 
            ITaxCalculator taxCalculator, IShippingCalculator shippingCalculator)
        {
            _getOrderService = getOrderService;
            _taxCalculator = taxCalculator;
            _shippingCalculator = shippingCalculator;
        }

        public decimal CalculateTotalPrice(int orderId)
        {
            // load order from database
            var order = _getOrderService.Get(orderId);
            var totalPriceOfAllProducts = order.TotalPriceOfAllProducts;

            // calculate tax
            decimal tax = _taxCalculator.CalculateTax(order);

            // calculate shipping
            decimal shippingCharges = _shippingCalculator.CalculateShipping(order);

            return totalPriceOfAllProducts + tax + shippingCharges;
        }
    }
}
