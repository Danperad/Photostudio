namespace PhotostudioDLL.Entities;

public class Hall : Costable
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
            s.StartTime!.Value, s.EndTime!.Value.AddHours(HoursWait))));
    }

    public static void Update()
    {
        ContextDb.Save();
    }

    #endregion

    #region Properties

    public int ID { get; set; }
    public virtual List<OrderService> Services { get; set; }

    #endregion

    #region Constructors

    public Hall()
    {
        Services = new List<OrderService>();
    }

    public Hall(string title, string description, decimal cost) : base(title, description, cost)
    {
        Services = new List<OrderService>();
    }

    #endregion
}