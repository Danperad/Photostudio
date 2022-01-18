using PhotostudioDLL.Entities.Interfaces;

namespace PhotostudioDLL.Entities.Services;

public class HallRentService : OrderService, ITimedService
{
    /// <summary>
    /// Проверка услуги аренды помещения
    /// </summary>
    /// <param name="service"></param>
    /// <returns></returns>
    internal static bool HallRentCheck(HallRentService service)
    {
        return service.Hall is not null && service.StartTime < service.EndTime &&
               service.Employee is not null;
    }
    
    public virtual Hall Hall { get; set; }
    public int HallID { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    
    public HallRentService(Employee employee, Service service, Order order,Hall hall, DateTime startTime, DateTime endTime) : base(
        employee, service, order)
    {
        Hall = hall;
        StartTime = startTime;
        EndTime = endTime;
    }
}