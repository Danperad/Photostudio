using PhotostudioDLL.Entities;

namespace PhotostudioDLL.Exceptions;

public class OrderDateException : Exception
{
    public OrderDateException(string message, Order order) : base(message)
    {
        OrderDate = order.DateTime;
        ContractDate = order.Contract.StartDate;
        Name = $"{order.Client.LastName} {order.Client.FirstName}";
    }

    public DateTime OrderDate { get; }
    public DateOnly ContractDate { get; }
    public string Name { get; }
}