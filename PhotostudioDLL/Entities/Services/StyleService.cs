using PhotostudioDLL.Entities.Interfaces;

namespace PhotostudioDLL.Entities.Services;

public class StyleService : OrderService, ITimedService
{
    /// <summary>
    /// Проверка услуги стилиста/визажиста
    /// </summary>
    /// <param name="service"></param>
    /// <returns></returns>
    internal static bool StyleCheck(StyleService service)
    {
        return service.StartTime < service.EndTime &&
               service.Employee is not null;
    }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }

    public StyleService(Employee employee, Service service, Order order, DateTime startTime, DateTime endTime) : base(
        employee, service, order)
    {
        StartTime = startTime;
        EndTime = endTime;
    }
}