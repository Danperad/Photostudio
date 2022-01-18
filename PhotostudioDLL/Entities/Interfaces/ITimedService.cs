namespace PhotostudioDLL.Entities.Interfaces;

public interface ITimedService
{
    DateTime StartTime { get; set; } 
    DateTime EndTime { get; set; }
}