using PhotostudioDLL.Entity;

namespace PhotostudioDLL.Exception;

public class OrderDateException : System.Exception
{
    public OrderDateException(string message, Order order) : base(message)
    {
        OrderDate = order.DateTime;
        ContractDate = order.Contract.StartDate;
        Name = $"{order.Client.LastName} {order.Client.FirstName}";
    }

    public DateTime OrderDate { get; }
    public DateTime ContractDate { get; }
    public string Name { get; }
}