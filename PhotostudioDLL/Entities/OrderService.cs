using PhotostudioDLL.Attributes;

namespace PhotostudioDLL.Entities;

public class OrderService
{
    public enum ServiceStatus
    {
        [StringValue("Завершена")] COMPLETE,
        [StringValue("Ожидает")] WAITING,
        [StringValue("В процессе")] INPROGRESS,
        [StringValue("Отменена")] CANCЕLED
    }

    #region Methods

    /// <summary>
    ///     Получение всех предоставляемых услуг
    /// </summary>
    /// <returns></returns>
    public static IEnumerable<OrderService> Get()
    {
        return ContextDb.GetExecuteableService();
    }

    public static IEnumerable<OrderService> GetWithServiceId(int id)
    {
        return Get().Where(s => s.Service.ID == id);
    }

    /// <summary>
    ///     Проверка услуги фотографирование/видеосъемки
    /// </summary>
    /// <param name="orderService"></param>
    /// <returns></returns>
    private static bool PhotoVideoCheck(OrderService orderService)
    {
        return orderService.PhotoLocation is not null && orderService.StartTime.HasValue &&
               orderService.EndTime.HasValue &&
               orderService.StartTime.Value < orderService.EndTime.Value &&
               orderService.Employee is not null;
    }

    /// <summary>
    ///     Проверка услуги аренды вещей
    /// </summary>
    /// <param name="orderService"></param>
    /// <returns></returns>
    private static bool ItemRentCheck(OrderService orderService)
    {
        return orderService.RentedItem is not null && orderService.StartTime.HasValue &&
               orderService.EndTime.HasValue &&
               orderService.StartTime.Value < orderService.EndTime.Value &&
               orderService.Employee is not null && orderService.Number is > 0;
    }

    /// <summary>
    ///     Проверка услуги аренды помещения
    /// </summary>
    /// <param name="orderService"></param>
    /// <returns></returns>
    private static bool HallRentCheck(OrderService orderService)
    {
        return orderService.Hall is not null && orderService.StartTime.HasValue &&
               orderService.EndTime.HasValue &&
               orderService.StartTime.Value < orderService.EndTime.Value &&
               orderService.Employee is not null;
    }

    /// <summary>
    ///     Проверка услуги на верность данных
    /// </summary>
    /// <param name="orderService"></param>
    /// <returns></returns>
    private static bool Check(OrderService orderService)
    {
        switch (orderService.Service.ID)
        {
            case 1:
            case 2:
            case 9:
            case 11:
            case 12:
                return PhotoVideoCheck(orderService);
            case 5:
            case 6:
            case 10:
                return ItemRentCheck(orderService);
            case 7:
                return HallRentCheck(orderService);
            default:
                return orderService.Employee is not null;
        }
    }

    /// <summary>
    ///     Проверка списка услуг
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static OrderService? CheckList(IEnumerable<OrderService> services)
    {
        return services.FirstOrDefault(service => !Check(service));
    }

    public static void Update()
    {
        ContextDb.Save();
    }

    #endregion

    #region Properties

    #region General

    public int ID { get; set; }

    public virtual Order Order { get; set; }
    public int OrderID { get; set; }
    public virtual Service Service { get; set; }
    public int ServiceID { get; set; }
    public virtual Employee Employee { get; set; }
    public int EmployeeID { get; set; }
    public ServiceStatus Status { get; set; }

    #endregion


    #region Rent

    public virtual RentedItem? RentedItem { get; set; }
    public int? RentedItemID { get; set; }
    public int? Number { get; set; }

    public virtual Hall? Hall { get; set; }
    public int? HallID { get; set; }

    public DateTime? StartTime { get; set; }
    public DateTime? EndTime { get; set; }

    #endregion


    #region Photo

    public string? PhotoLocation { get; set; }

    #endregion


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

    #endregion

    #region Constructors

    public OrderService()
    {
        Status = ServiceStatus.WAITING;
    }

    public OrderService(Employee employee, Service service, Order order) : this()
    {
        Employee = employee;
        Service = service;
        Order = order;
    }

    public OrderService(Employee employee, Service service, Order order, Hall hall,
        DateTime startTime, DateTime endTime) : this(employee, service, order)
    {
        Hall = hall;
        StartTime = startTime;
        EndTime = endTime;
    }

    public OrderService(Employee employee, Service service, Order order, RentedItem rentedItem,
        DateTime startTime, DateTime endTime, int number) : this(employee, service, order)
    {
        RentedItem = rentedItem;
        StartTime = startTime;
        EndTime = endTime;
        Number = number;
    }

    public OrderService(Employee employee, Service service, Order order, RentedItem rentedItem,
        DateTime photoStartDateTime,
        DateTime photoEndDateTime) : this(employee, service, order)
    {
        RentedItem = rentedItem;
        StartTime = photoStartDateTime;
        EndTime = photoEndDateTime;
    }

    #endregion
}