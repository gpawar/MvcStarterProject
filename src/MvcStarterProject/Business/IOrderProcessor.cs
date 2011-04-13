namespace MvcStarterProject.Business
{
    public interface IOrderProcessor
    {
        decimal CalculateTotalPrice(int orderId);
    }
}