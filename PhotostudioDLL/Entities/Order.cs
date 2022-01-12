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

    public static bool Add(Order order)
    {
        if (!Check(order)) return false;
        if (order.Services.Any(service => !ExecuteableService.Check(service)))
        {
            return false;
        }
        ContextDb.Add(order);
        return true;
    }

    private static bool Check(Order order)
    {
        return Contract.Check(order.Contract) && DateOnly.FromDateTime(order.DateTime) <= order.Contract.StartDate;
    }

    public static List<Order> Get()
    {
        return ContextDb.GetOrders();
    }

    public static void Update()
    {
        ContextDb.Save();
    }

    #endregion

    #region Properties

    public int ID { get; set; }
    public virtual Contract Contract { get; set; }
    public virtual Client Client { get; set; }
    public int ClientID { get; set; }
    public DateTime DateTime { get; set; }
    public OrderStatus Status { get; set; }
    public virtual List<ExecuteableService> Services { get; set; }
    
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
                    cost += service.RentedItem.Cost * service.Number!.Value *
                            (decimal)(service.EndRent - service.StartRent)!.Value.TotalHours;
                if (service.Hall != null)
                    cost += service.Hall.Cost * (decimal)(service.EndRent - service.StartRent)!.Value.TotalHours;
                if (service.PhotoLocation != null)
                    cost += service.Service.Cost *
                            ((decimal)(service.EndRent - service.StartRent)!.Value.TotalHours - 1);
            }

            return cost;
        }
    }

    /// <summary>
    /// Свойство находящее описание статуса используя рефлексию
    /// </summary>
    public string? TextStatus
    {
        get
        {
            var type = Status.GetType();
            var fieldInfo = type.GetField(Status.ToString());
            var attribs = fieldInfo!.GetCustomAttributes(
                typeof(StringValueAttribute), false) as StringValueAttribute[];
            return attribs!.Length > 0 ? attribs[0].StringValue : null;
        }
    }

    public string ListServices
    {
        get
        {
            if (Services.Count == 0) return "";
            var temp = Services.Aggregate(string.Empty, (current, service) => current + $"{service.Service.Title}, ");
            return temp.Remove(temp.Length - 2, 2);
        }
    }

    #endregion

    #region Constructors

    public Order()
    {
        Services = new List<ExecuteableService>();
        Status = OrderStatus.PREWORK;
    }

    public Order(Contract contract, Client client, DateTime dateTime, List<ExecuteableService> services)
    {
        Status = OrderStatus.PREWORK;
        Contract = contract;
        Client = client;
        DateTime = dateTime;
        Services = services;
    }

    #endregion
}