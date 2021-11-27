using System;
using PhotostudioDLL.Entity;

namespace PhotostudioDLL.Exception
{
    public class ContractDateException : System.Exception
    {
        public ContractDateException(string message, Contract contract) : base(message)
        {
            StartDate = contract.StartDate;
            EndDate = contract.EndDate;
        }

        public DateTime StartDate { get; }
        public DateTime EndDate { get; }
    }
}