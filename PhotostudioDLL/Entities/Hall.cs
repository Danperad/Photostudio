using PhotostudioDLL.Entities.Interfaces;
using PhotostudioDLL.Entities.Services;

namespace PhotostudioDLL.Entities;

public class Hall : ICostable
{
    // Время между двумя новыми заявками
    private const int HoursWait = 2;

    #region Methods

    /// <summary>
    ///     Получение всех залов
    /// </summary>
    /// <returns></returns>
    public static IEnumerable<Hall> Get()
    {
        return ContextDb.GetHalls();
    }

    /// <summary>
    ///     Получение доступных залов
    /// </summary>
    /// <param name="start"></param>
    /// <param name="end"></param>
    /// <returns></returns>
    public static IEnumerable<Hall> GetWithTime(DateTime start, DateTime end)
    {
        return ContextDb.GetHalls().Where(e => !e.Services.Any(s => ContextDb.FindDateTime(start,
            end.AddHours(HoursWait),
            s.StartTime, s.EndTime.AddHours(HoursWait))));
    }

    public static void Update()
    {
        ContextDb.Save();
    }

    #endregion

    #region Properties

    public int ID { get; set; }
    public string Title { get; set; }
    public decimal? Cost { get; set; }
    public string Description { get; set; }
    public virtual List<HallRentService> Services { get; set; }

    #endregion

    #region Constructors

    public Hall()
    {
        Services = new List<HallRentService>();
    }

    public Hall(string title, string description, decimal cost) : this()
    {
        Title = title;
        Description = description;
        Cost = cost;
    }

    #endregion
}