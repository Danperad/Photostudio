using System.ComponentModel.DataAnnotations;
using PhotostudioDLL.Exceptions;

namespace PhotostudioDLL.Entities;

public class ServiceProvided
{
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


    #region Properties

    public int ID { get; set; }

    [Required] public virtual Order Order { get; set; }
    public int OrderID { get; set; }
    [Required] public virtual Service Service { get; set; }
    public int ServiceID { get; set; }
    public string? Description { get; set; }
    [Required] public virtual Employee Employee { get; set; }
    public int EmployeeID { get; set; }

    public virtual RentedItem? RentedItem { get; set; }
    public int? RentedItemID { get; set; }
    public int? Number { get; set; }

    public virtual Hall? Hall { get; set; }
    public int? HallID { get; set; }

    public DateTime? StartRent { get; set; }
    public DateTime? EndRent { get; set; }

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

    public ServiceProvided(Employee Employee, Service Service, Order Order, Hall Hall, DateTime StartRent,
        DateTime EndRent)
    {
        this.Employee = Employee;
        this.Service = Service;
        this.Order = Order;
        this.Hall = Hall;
        this.StartRent = StartRent;
        this.EndRent = EndRent;
    }

    public ServiceProvided(Employee Employee, Service Service, Order Order, RentedItem RentedItem, DateTime StartRent,
        DateTime EndRent, int Number)
    {
        this.Employee = Employee;
        this.Service = Service;
        this.Order = Order;
        this.RentedItem = RentedItem;
        this.StartRent = StartRent;
        this.EndRent = EndRent;
        this.Number = Number;
    }

    public ServiceProvided(Employee Employee, Service Service, Order Order, string PhotoLocation, DateTime StartRent,
        DateTime EndRent)
    {
        this.Employee = Employee;
        this.Service = Service;
        this.Order = Order;
        RentedItem = RentedItem;
        this.StartRent = StartRent;
        this.EndRent = EndRent;
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