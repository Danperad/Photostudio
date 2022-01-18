using PhotostudioDLL.Entities.Interfaces;

namespace PhotostudioDLL.Entities.Services;

public class RentService : OrderService, ITimedService
{
    /// <summary>
    /// Проверка услуги аренды вещей
    /// </summary>
    /// <param name="service"></param>
    /// <returns></returns>
    internal static bool ItemRentCheck(RentService service)
    {
        return service.RentedItem is not null && service.StartTime< service.EndTime &&
               service.Employee is not null && service.Number > 0;
    }
    
    public virtual RentedItem RentedItem { get; set; }
    public int RentedItemID { get; set; }
    public int Number { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    
    public RentService(Employee employee, Service service, Order order,RentedItem rentedItem, int number, DateTime startTime, DateTime endTime) : base(
        employee, service, order)
    {
        RentedItem = rentedItem;
        Number = number;
        StartTime = startTime;
        EndTime = endTime;
    }
}