namespace PhotostudioDLL.Entities;

public class RentedItem : Costable
{
    // Время между двумя новыми заявками
    private const int HoursWait = 3;

    #region Methods

    /// <summary>
    ///     Добавление арендуемой вещи
    /// </summary>
    /// <param name="rentedItem"></param>
    /// <returns></returns>
    public static bool Add(RentedItem rentedItem)
    {
        if (!Check(rentedItem)) return false;
        ContextDb.Add(rentedItem);
        return true;
    }

    /// <summary>
    ///     Получение всех арендуемых вещей
    /// </summary>
    /// <returns></returns>
    public static List<RentedItem> Get()
    {
        return ContextDb.GetRentedItems();
    }

    /// <summary>
    ///     Получение доступных арендуемых вещей
    /// </summary>
    /// <param name="start"></param>
    /// <param name="end"></param>
    /// <returns></returns>
    private static IEnumerable<RentedItem> GetWithTime(DateTime start, DateTime end)
    {
        return ContextDb.GetRentedItems()
            .Where(r => r.Number - r.Services
                .Where(s => ContextDb.FindDateTime(start, end.AddHours(HoursWait),
                    s.StartTime!.Value, s.EndTime!.Value.AddHours(HoursWait)))
                .Sum(s => s.Number) > 0);
    }

    /// <summary>
    ///     Получение доступного реквизита
    /// </summary>
    /// <param name="start"></param>
    /// <param name="end"></param>
    /// <returns></returns>
    public static IEnumerable<RentedItem> GetNoDress(DateTime start, DateTime end)
    {
        return GetWithTime(start, end).Where(r => !r.IsСlothes);
    }

    /// <summary>
    ///     Получение доступной одежды
    /// </summary>
    /// <param name="start"></param>
    /// <param name="end"></param>
    /// <returns></returns>
    public static IEnumerable<RentedItem> GetСlothes(DateTime start, DateTime end)
    {
        return GetWithTime(start, end).Where(r => r.IsСlothes && !r.IsKids);
    }

    /// <summary>
    ///     Получение доступной детской одежды
    /// </summary>
    /// <param name="start"></param>
    /// <param name="end"></param>
    /// <returns></returns>
    public static IEnumerable<RentedItem> GetKidsСlothes(DateTime start, DateTime end)
    {
        return GetWithTime(start, end).Where(r => r.IsСlothes && r.IsKids);
    }

    /// <summary>
    ///     Получение арендуемой вещи по id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public static RentedItem? GetById(int id)
    {
        return ContextDb.GetRentedItems().FirstOrDefault(r => r.ID == id);
    }

    public static void Update()
    {
        ContextDb.Save();
    }

    /// <summary>
    ///     Получение числа доступных экземпляров
    /// </summary>
    /// <param name="start"></param>
    /// <param name="end"></param>
    /// <returns></returns>
    public int GetAvailable(DateTime start, DateTime end)
    {
        return (int) (Number - Services.Where(s => ContextDb.FindDateTime(start, end.AddHours(HoursWait),
            s.StartTime!.Value, s.EndTime!.Value.AddHours(HoursWait))).Sum(s => s.Number!.Value));
    }

    #endregion

    #region Properties

    public int ID { get; set; }
    public uint Number { get; set; }
    public bool IsСlothes { get; set; }
    public bool IsKids { get; set; }

    public virtual List<OrderService> Services { get; set; }

    #endregion

    #region Constructors

    public RentedItem()
    {
        Services = new List<OrderService>();
    }

    public RentedItem(string title, string description, uint number, decimal cost, bool isСlothes, bool isKids) : base(
        title,
        description, cost)
    {
        Number = number;
        IsСlothes = isСlothes;
        IsKids = isKids;
        Services = new List<OrderService>();
    }

    #endregion
}