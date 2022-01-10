using PhotostudioDLL.Attributes;
using PhotostudioDLL.Exceptions;

namespace PhotostudioDLL.Entities;

public class Order
{
    public enum OrderStatus
    {
        [StringValue("Завершена")] COMPLETE,
        [StringValue("Подготовка")] PREWORK,
        [StringValue("Фотографирование")] INPHOTO,
        [StringValue("Создание Видео")] INVIDEO,
        [StringValue("На ретушировании")] INRETUSH,
        [StringValue("Отменён")] CLOSED,
        [StringValue("Печать")] PRINT
    }

    #region Methods

    public static void Add(Order order)
    {
        Check(order);
        ContextDB.Add(order);
    }

    private static void Check(Order order)
    {
        Contract.Check(order.Contract);
        if (DateOnly.FromDateTime(order.DateTime) > order.Contract.StartDate)
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

    #endregion

    #region Properties

    public int ID { get; set; }
    public virtual Contract Contract { get; set; }
    public int ContractID { get; set; }
    public virtual Client Client { get; set; }
    public int ClientID { get; set; }
    public DateTime DateTime { get; set; }
    public OrderStatus Status { get; set; }
    public virtual List<ServiceProvided> Services { get; set; }
    
    /// <summary>
    /// Своство расчитываемое общую стоимость заявки
    /// </summary>
    public decimal AllGetCost
    {
        get
        {
            decimal cost = 0;
            foreach (var service in Services)
            {
                cost += service.Service.Cost;
                if (service.RentedItem != null)
                    cost += service.RentedItem.Cost * service.Number.Value *
                            (decimal)(service.EndRent - service.StartRent).Value.TotalHours;
                if (service.Hall != null)
                    cost += service.Hall.Cost * (decimal)(service.EndRent - service.StartRent).Value.TotalHours;
                if (service.PhotoLocation != null)
                    cost += service.Service.Cost *
                            ((decimal)(service.EndRent - service.StartRent).Value.TotalHours - 1);
            }

            return cost;
        }
    }

    /// <summary>
    /// Свойство находящее описание статуса используя рефлексию
    /// </summary>
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
        Status = OrderStatus.PREWORK;
    }

    public Order(Contract Contract, Client Client, DateTime DateTime, List<ServiceProvided> Services)
    {
        this.Contract = Contract;
        ContractID = Contract.ID;
        this.Client = Client;
        this.DateTime = DateTime;
        Status = OrderStatus.PREWORK;
        this.Services = Services;
    }

    #endregion
}