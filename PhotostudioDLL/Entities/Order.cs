using System.ComponentModel.DataAnnotations;
using PhotostudioDLL.Attributes;
using PhotostudioDLL.Exceptions;

namespace PhotostudioDLL.Entities;

public class Order
{
    public enum OrderStatus
    {
        [StringValue("В процессе")] COMPLETE,
        [StringValue("В процессе")] INPROGRESS
    }

    public static void Add(Order order)
    {
        Check(order);
        ContextDB.Add(order);
    }

    private static void Check(Order order)
    {
        Contract.Check(order.Contract);
        if (DateOnly.FromDateTime(order.DateTime) < order.Contract.StartDate)
            throw new OrderDateException("OrderDateError", order);
    }

    public static List<Order> Get()
    {
        return ContextDB.GetOrders();
    }

    public static void Update()
    {
        ContextDB.Save();
    }

    public decimal GetCost()
    {
        decimal cost = 0;
        foreach (var service in Services)
        {
            cost += service.Service.GetCost();
            if (service.RentedItem != null)
                cost += service.RentedItem.GetCost() * service.Number.Value *
                        (decimal)(service.EndRent - service.StartRent).Value.TotalHours;
            if (service.Hall != null)
                cost += service.Hall.GetCost() * (decimal)(service.EndRent - service.StartRent).Value.TotalHours;
            if (service.PhotoLocation != null)
                cost += service.Service.GetCost() *
                        ((decimal)(service.EndRent - service.StartRent).Value.TotalHours - 1);
        }

        return cost;
    }

    #region Properties

    public int ID { get; set; }
    [Required] public virtual Contract Contract { get; set; }
    public int ContractID { get; set; }
    [Required] public virtual Client Client { get; set; }
    public int ClientID { get; set; }
    [Required] public DateTime DateTime { get; set; }
    [Required] public OrderStatus Status { get; set; }
    [Required] public virtual List<ServiceProvided> Services { get; set; }

    public string TextStatus
    {
        get
        {
            var type = Status.GetType();
            var fieldInfo = type.GetField(Status.ToString());
            var attribs = fieldInfo.GetCustomAttributes(
                typeof(StringValueAttribute), false) as StringValueAttribute[];
            return attribs.Length > 0 ? attribs[0].StringValue : null;
        }
    }

    public string ListServices
    {
        get
        {
            if (Services.Count == 0) return "";
            var temp = string.Empty;
            foreach (var service in Services) temp += $"{service.Service.Title}, ";

            return temp.Remove(temp.Length - 2, 2);
        }
    }

    #endregion

    #region Constructors

    public Order()
    {
        Services = new List<ServiceProvided>();
        Status = OrderStatus.INPROGRESS;
    }

    public Order(Contract Contract, Client Client, DateTime DateTime, List<ServiceProvided> Services)
    {
        this.Contract = Contract;
        this.Client = Client;
        this.DateTime = DateTime;
        Status = OrderStatus.INPROGRESS;
        this.Services = Services;
        foreach (var service in Services) ServiceProvided.Add(service);
    }

    #endregion
}