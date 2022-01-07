using System.ComponentModel.DataAnnotations;
using System.Reflection;
using PhotostudioDLL.Attribute;
using PhotostudioDLL.Exception;

namespace PhotostudioDLL.Entity;

public class Order
{
    public int ID { get; set; }

    [Required] public virtual Contract Contract { get; set; }
    [Required] public virtual Client Client { get; set; }
    [Required] public DateTime DateTime { get; set; }
    [Required] public OrderStatus Status { get; set; }
    [Required] public virtual List<ServiceProvided> Services { get; set; }

    public string TextStatus
    {
        get
        {
            Type type = Status.GetType();
            FieldInfo fieldInfo = type.GetField(Status.ToString());
            StringValueAttribute[] attribs = fieldInfo.GetCustomAttributes(
                typeof(StringValueAttribute), false) as StringValueAttribute[];
            return attribs.Length > 0 ? attribs[0].StringValue : null;
        }
    }

    public string ListServices
    {
        get
        {
            if (Services.Count == 0) return "";
            string temp = string.Empty;
            foreach (var service in Services)
            {
                temp += $"{service.Service.Title}, ";
            }

            return temp.Remove(temp.Length - 2, 2);
        }
    }

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
        foreach (var service in Services)
        {
            ServiceProvided.Add(service);
        }
    }

    public enum OrderStatus
    {
        [StringValue("В процессе")] COMPLETE,
        [StringValue("В процессе")] INPROGRESS,
    }

    public static void Add(Order order)
    {
        Check(order);
        Add(order);
    }

    private static void Check(Order order)
    {
        Entity.Contract.Check(order.Contract);
        if (DateOnly.FromDateTime(order.DateTime) < order.Contract.StartDate)
            throw new OrderDateException("OrderDateError", order);
    }

    public static List<Order> Get() => ContextDB.GetOrders();
}