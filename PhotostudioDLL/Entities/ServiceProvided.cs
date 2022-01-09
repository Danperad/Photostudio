using PhotostudioDLL.Exceptions;

namespace PhotostudioDLL.Entities;

public class ServiceProvided
{
    #region Methods

    public static void Add(ServiceProvided serviceProvided)
    {
        Check(serviceProvided);
        ContextDB.Add(serviceProvided);
    }

    public static List<ServiceProvided> Get()
    {
        return ContextDB.GetServiceProvideds();
    }

    private static void Check(ServiceProvided service)
    {
        if (service.PhotoLocation != null && service.PhotoStartDateTime > service.PhotoEndDateTime)
            throw new ServiceProvidedExeption("PhotoTimeError", service);
        if ((service.RentedItem != null || service.Hall != null) && service.StartRent > service.EndRent)
            throw new ServiceProvidedExeption("RentTimeError", service);
    }

    public static void Update()
    {
        ContextDB.Save();
    }

    #endregion

    #region Properties

    public int ID { get; set; }

    public virtual Order Order { get; set; }
    public int OrderID { get; set; }
    public virtual Service Service { get; set; }
    public int ServiceID { get; set; }
    public string? Description { get; set; }
    public virtual Employee Employee { get; set; }
    public int EmployeeID { get; set; }

    public virtual RentedItem? RentedItem { get; set; }
    public int? RentedItemID { get; set; }
    public int? Number { get; set; }

    public virtual Hall? Hall { get; set; }
    public int? HallID { get; set; }

    public DateOnly? RentDate { get; set; }
    public TimeOnly? StartRent { get; set; }
    public TimeOnly? EndRent { get; set; }

    public string? PhotoLocation { get; set; }
    public DateTime? PhotoStartDateTime { get; set; }
    public DateTime? PhotoEndDateTime { get; set; }

    #endregion

    #region Constructors

    public ServiceProvided()
    {
    }

    public ServiceProvided(Employee Employee, Service Service, Order Order)
    {
        this.Employee = Employee;
        this.Service = Service;
        this.Order = Order;
    }

    public ServiceProvided(Employee Employee, Service Service, Order Order, Hall Hall, DateOnly RentDate,
        TimeOnly StartRent, TimeOnly EndRent)
    {
        this.Employee = Employee;
        this.Service = Service;
        this.Order = Order;
        this.Hall = Hall;
        this.RentDate = RentDate;
        this.StartRent = StartRent;
        this.EndRent = EndRent;
    }

    public ServiceProvided(Employee Employee, Service Service, Order Order, RentedItem RentedItem, DateOnly RentDate,
        TimeOnly StartRent, TimeOnly EndRent, int Number)
    {
        this.Employee = Employee;
        this.Service = Service;
        this.Order = Order;
        this.RentedItem = RentedItem;
        this.RentDate = RentDate;
        this.StartRent = StartRent;
        this.EndRent = EndRent;
        this.Number = Number;
    }

    public ServiceProvided(Employee Employee, Service Service, Order Order, RentedItem RentedItem,
        DateTime PhotoStartDateTime,
        DateTime PhotoEndDateTime)
    {
        this.Employee = Employee;
        this.Service = Service;
        this.Order = Order;
        this.RentedItem = RentedItem;
        this.PhotoStartDateTime = PhotoStartDateTime;
        this.PhotoEndDateTime = PhotoEndDateTime;
    }

    #endregion
}