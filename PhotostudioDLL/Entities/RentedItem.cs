using PhotostudioDLL.Entities.Interfaces;
using PhotostudioDLL.Entities.Services;

namespace PhotostudioDLL.Entities;

public class RentedItem : ICostable
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
        if (!ICostable.Check(rentedItem)) return false;
        ContextDb.Add(rentedItem);
        return true;
    }

    /// <summary>
    ///     Получение всех арендуемых вещей
    /// </summary>
    /// <returns></returns>
    public static IEnumerable<RentedItem> Get()
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
                    s.StartTime, s.EndTime.AddHours(HoursWait)))
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
            s.StartTime, s.EndTime.AddHours(HoursWait))).Sum(s => s.Number));
    }

    #endregion

    #region Properties

    public int ID { get; set; }
    public string Title { get; set; }
    public decimal? Cost { get; set; }
    public string Description { get; set; }
    public uint Number { get; set; }
    public bool IsСlothes { get; set; }
    public bool IsKids { get; set; }

    public virtual List<RentService> Services { get; set; }

    #endregion

    #region Constructors

    public RentedItem()
    {
        Services = new List<RentService>();
    }

    public RentedItem(string title, string description, uint number, decimal cost, bool isСlothes, bool isKids) : this()
    {
        Title = title;
        Description = description;
        Cost = cost;
        Number = number;
        IsСlothes = isСlothes;
        IsKids = isKids;
    }

    #endregion
}