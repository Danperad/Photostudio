using System;
using PhotostudioDLL.Entity;

namespace PhotostudioDLL.Exceptions
{
    public class OrderDateException : Exception
    {
        public DateTime OrderDate { get; }
        public DateTime ContractDate { get; }
        private string _orderClientName;
        public OrderDateException(string message, Order order) : base(message)
        {
            OrderDate = order.DateTime;
            ContractDate = order.Contract.StartDate;
            _orderClientName = $"{order.Client.LastName} {order.Client.FirstName}";
        }
        public string getClientName()
        {
            return _orderClientName;
        }
    }
}