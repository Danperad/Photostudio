namespace PhotostudioDLL.Entities;

public sealed class ExecuteableService
{
    #region Methods

    public static List<ExecuteableService> Get()
    {
        return ContextDb.GetExecuteableService();
    }

    internal static bool Check(ExecuteableService executeableService)
    {
        if (executeableService.PhotoLocation != null &&
            executeableService.PhotoStartDateTime > executeableService.PhotoEndDateTime)
            return false;
        return executeableService.RentedItem == null && executeableService.Hall == null ||
               !(executeableService.StartRent > executeableService.EndRent);
    }

    public static void Update()
    {
        ContextDb.Save();
    }

    #endregion

    #region Properties

    public int ID { get; set; }

    public Order Order { get; set; }
    public int OrderID { get; set; }
    public Service Service { get; set; }
    public int ServiceID { get; set; }
    public string? Description { get; set; }
    public Employee Employee { get; set; }
    public int EmployeeID { get; set; }

    public RentedItem? RentedItem { get; set; }
    public int? RentedItemID { get; set; }
    public int? Number { get; set; }

    public Hall? Hall { get; set; }
    public int? HallID { get; set; }

    public DateOnly? RentDate { get; set; }
    public TimeOnly? StartRent { get; set; }
    public TimeOnly? EndRent { get; set; }

    public string? PhotoLocation { get; set; }
    public DateTime? PhotoStartDateTime { get; set; }
    public DateTime? PhotoEndDateTime { get; set; }

    #endregion

    #region Constructors

    public ExecuteableService()
    {
    }

    public ExecuteableService(Employee employee, Service service, Order order)
    {
        Employee = employee;
        Service = service;
        Order = order;
    }

    public ExecuteableService(Employee employee, Service service, Order order, Hall hall, DateOnly rentDate,
        TimeOnly startRent, TimeOnly endRent) : this(employee, service, order)
    {
        Hall = hall;
        RentDate = rentDate;
        StartRent = startRent;
        EndRent = endRent;
    }

    public ExecuteableService(Employee employee, Service service, Order order, RentedItem rentedItem, DateOnly rentDate,
        TimeOnly startRent, TimeOnly endRent, int number) : this(employee, service, order)
    {
        RentedItem = rentedItem;
        RentDate = rentDate;
        StartRent = startRent;
        EndRent = endRent;
        Number = number;
    }

    public ExecuteableService(Employee employee, Service service, Order order, RentedItem rentedItem,
        DateTime photoStartDateTime,
        DateTime photoEndDateTime) : this(employee, service, order)
    {
        RentedItem = rentedItem;
        PhotoStartDateTime = photoStartDateTime;
        PhotoEndDateTime = photoEndDateTime;
    }

    #endregion
}