namespace MvcStarterProject.Business
{
    public interface ITaxCalculator
    {
        decimal CalculateTax(Order order);
    }
}