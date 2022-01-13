using PhotostudioDLL.Attributes;

namespace PhotostudioDLL.Entities;

public class Order
{
    public enum OrderStatus
    {
        [StringValue("Завершена")] COMPLETE,
        [StringValue("Подготовка")] PREWORK,
        [StringValue("В работе")] INWORK,
        [StringValue("Отменен")] CLOSED
    }

    #region Methods

    /// <summary>
    ///     Проверка и изменения статуса заявок
    /// </summary>
    public static void CheckStatusTime()
    {
        var orders = Get();
        foreach (var order in orders)
        {
            foreach (var service in order.Services)
                if (service.Status == OrderService.ServiceStatus.INPROGRESS && service.EndTime.HasValue &&
                    service.EndTime.Value < DateTime.Now)
                    service.Status = OrderService.ServiceStatus.COMPLETE;

            if (order.Services.All(s => s.Status == OrderService.ServiceStatus.COMPLETE))
                order.Status = OrderStatus.COMPLETE;
            if (order.Status != OrderStatus.COMPLETE &&
                order.Contract.EndDate < DateOnly.FromDateTime(DateTime.Today))
            {
                order.Status = OrderStatus.CLOSED;
                foreach (var service in order.Services) service.Status = OrderService.ServiceStatus.CANCЕLED;
            }
        }

        Update();
    }

    /// <summary>
    ///     Добавление новой заявки
    /// </summary>
    /// <param name="order"></param>
    /// <returns></returns>
    public static bool Add(Order order)
    {
        if (!Check(order)) return false;
        ContextDb.Add(order);
        return true;
    }

    /// <summary>
    ///     Проверка заявки
    /// </summary>
    /// <param name="order"></param>
    /// <returns></returns>
    private static bool Check(Order order)
    {
        return Contract.Check(order.Contract) && DateOnly.FromDateTime(order.DateTime) <= order.Contract.StartDate;
    }

    /// <summary>
    ///     Получение всех заявок
    /// </summary>
    /// <returns></returns>
    public static List<Order> Get()
    {
        return ContextDb.GetOrders();
    }

    /// <summary>
    ///     Установка новых значение в базе данных
    /// </summary>
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
    public virtual List<OrderService> Services { get; set; }

    /// <summary>
    ///     Своство расчитываемое общую стоимость заявки
    /// </summary>
    public decimal AllGetCost
    {
        get
        {
            decimal cost = 0;
            foreach (var service in Services)
            {
                cost += service.Service.Cost.GetValueOrDefault(0);
                if (service.RentedItem != null)
                    cost += service.RentedItem.Cost!.Value * service.Number!.Value *
                            (decimal) (service.EndTime - service.StartTime)!.Value.TotalHours;
                if (service.Hall != null)
                    cost += service.Hall.Cost!.Value *
                            (decimal) (service.EndTime - service.StartTime)!.Value.TotalHours;
                if (service.PhotoLocation != null)
                    cost += service.Service.Cost!.Value *
                            (decimal) (service.StartTime - service.EndTime)!.Value.TotalHours;
            }

            return cost;
        }
    }

    /// <summary>
    ///     Свойство находящее описание статуса используя рефлексию
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
        Services = new List<OrderService>();
        Status = OrderStatus.PREWORK;
    }

    public Order(Contract contract, Client client, DateTime dateTime, List<OrderService> services)
    {
        Status = OrderStatus.PREWORK;
        Contract = contract;
        Client = client;
        DateTime = dateTime;
        Services = services;
    }

    #endregion
}