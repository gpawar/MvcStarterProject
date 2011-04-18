namespace MvcStarterProject.Business
{
    public interface IOrderProcessor
    {
        decimal SubtotalBeforeTaxAndShipping(Order order);
        decimal ShippingCharges(Order order);
        decimal Tax(Order order);
        decimal TotalPrice(Order order);
    }
}