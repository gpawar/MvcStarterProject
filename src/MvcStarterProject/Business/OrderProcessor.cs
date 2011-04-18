using System;
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

        public decimal SubtotalBeforeTaxAndShipping(Order order)
        {
            return order.TotalPriceOfAllProducts;
        }

        public decimal ShippingCharges(Order order)
        {
            return _shippingCalculator.CalculateShipping(order);
        }

        public decimal Tax(Order order)
        {
            return _taxCalculator.CalculateTax(order);
        }

        public decimal TotalPrice(Order order)
        {
            return SubtotalBeforeTaxAndShipping(order) + ShippingCharges(order) + Tax(order);
        }
    }
}
