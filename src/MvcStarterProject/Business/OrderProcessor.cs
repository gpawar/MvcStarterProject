using MvcStarterProject.DataAccess;

namespace MvcStarterProject.Business
{
    public class OrderProcessor : IOrderProcessor
    {
        private readonly IRepository<Order> _orderRepository;
        private readonly ITaxCalculator _taxCalculator;
        private readonly IShippingCalculator _shippingCalculator;

        public OrderProcessor(IRepository<Order> orderRepository, ITaxCalculator taxCalculator, IShippingCalculator shippingCalculator)
        {
            _orderRepository = orderRepository;
            _taxCalculator = taxCalculator;
            _shippingCalculator = shippingCalculator;
        }

        public decimal CalculateTotalPrice(int orderId)
        {
            // load order from database
            var order = _orderRepository.Get(orderId);
            var totalPriceOfAllProducts = order.TotalPriceOfAllProducts;

            // calculate tax
            decimal tax = _taxCalculator.CalculateTax(order);

            // calculate shipping
            decimal shippingCharges = _shippingCalculator.CalculateShipping(order);

            return totalPriceOfAllProducts + tax + shippingCharges;
        }
    }
}
