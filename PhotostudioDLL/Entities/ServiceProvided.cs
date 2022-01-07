using System.ComponentModel.DataAnnotations;
using PhotostudioDLL.Exception;

namespace PhotostudioDLL.Entity;

public class ServiceProvided
{
    #region Properties

    public int ID { get; set; }

    [Required] public virtual Order Order { get; set; }
    [Required] public virtual Service Service { get; set; }
    public string? Description { get; set; }
    [Required] public virtual Employee Employee { get; set; }

    public virtual RentedItem? RentedItem { get; set; }
    public int? Number { get; set; }

    public virtual Hall? Hall { get; set; }
    public DateTime? StartRent { get; set; }
    public DateTime? EndRent { get; set; }

    public string? PhotoLocation { get; set; }
    public DateTime? PhotoStartDateTime { get; set; }
    public DateTime? PhotoEndDateTime { get; set; }

    #endregion
    
    public ServiceProvided()
    {
    }

    public ServiceProvided(Employee Employee, Service Service, Order Order)
    {
        this.Employee = Employee;
        this.Service = Service;
        this.Order = Order;
    }

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
}