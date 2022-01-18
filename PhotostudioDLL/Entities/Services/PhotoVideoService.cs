using PhotostudioDLL.Entities.Interfaces;

namespace PhotostudioDLL.Entities.Services;

public class PhotoVideoService : OrderService, ITimedService
{
    /// <summary>
    /// Проверка услуги фотографирование/видеосъемки
    /// </summary>
    /// <param name="service"></param>
    /// <returns></returns>
    internal static bool PhotoVideoCheck(PhotoVideoService service)
    {
        return service.PhotoLocation is not null && 
               service.StartTime < service.EndTime &&
               service.Employee is not null;
    }
    
    public string PhotoLocation { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }

    public PhotoVideoService(Employee employee, Service service, Order order, string photoLocation, DateTime startTime,
        DateTime endTime) : base(employee, service, order)
    {
        PhotoLocation = photoLocation;
        StartTime = startTime;
        EndTime = endTime;
    }
}