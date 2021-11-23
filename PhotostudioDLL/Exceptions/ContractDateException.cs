using System;
using PhotostudioDLL.Entity;

namespace PhotostudioDLL.Exceptions
{
    public class ContractDateException : Exception
    {
        public DateTime FirstDate { get; }
        public DateTime SecondDate { get; }
        private string _orderClientName;
        public ContractDateException(string message, Order order) : base(message)
        {
            FirstDate = order.Contract.StartDate;
            SecondDate = order.Contract.EndDate;
            _orderClientName = $"{order.Client.LastName} {order.Client.FirstName}";
        }
        public ContractDateException(string message, Contract contract) : base(message)
        {
            FirstDate = contract.StartDate;
            SecondDate = contract.EndDate;
        }

        public string getClientName()
        {
            return _orderClientName;
        }
    }
}