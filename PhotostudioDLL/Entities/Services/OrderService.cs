using PhotostudioDLL.Attributes;

namespace PhotostudioDLL.Entities.Services;

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
    ///     Проверка услуги на верность данных
    /// </summary>
    /// <param name="orderService"></param>
    /// <returns></returns>
    private static bool Check(OrderService orderService)
    {
        return orderService.Service.Type switch
        {
            Service.ServiceType.PHOTOVIDEO => PhotoVideoService.PhotoVideoCheck(orderService as PhotoVideoService),
            Service.ServiceType.RENT => RentService.ItemRentCheck(orderService as RentService),
            Service.ServiceType.HALLRENT => HallRentService.HallRentCheck(orderService as HallRentService),
            Service.ServiceType.STYLE => StyleService.StyleCheck(orderService as StyleService),
            _ => orderService.Employee is not null
        };
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
    
    public int ID { get; set; }

    public virtual Order Order { get; set; }
    public int OrderID { get; set; }
    public virtual Service Service { get; set; }
    public int ServiceID { get; set; }
    public virtual Employee Employee { get; set; }
    public int EmployeeID { get; set; }
    public ServiceStatus Status { get; set; }
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

    #endregion
}