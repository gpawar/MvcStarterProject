namespace MvcStarterProject.Business
{
    public interface IShippingCalculator
    {
        decimal CalculateShipping(Order order);
    }
}