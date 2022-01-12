namespace PhotostudioDLL.Entities;

public class RentedItem : Costable
{
    #region Methods

    public static bool Add(RentedItem rentedItem)
    {
        if (!Check(rentedItem)) return false;
        ContextDb.Add(rentedItem);
        return true;
    }

    public static List<RentedItem> Get()
    {
        return ContextDb.GetRentedItems();
    }

    private static List<RentedItem> GetWithTime(DateOnly date, TimeOnly startTime, TimeOnly endTime)
    {
        return ContextDb.GetRentedItems()
            .Where(r => r.Number - r.Services
                .Where(s => s.RentDate == date && (s.StartRent <= startTime ||
                                                   s.EndRent >= endTime ||
                                                   s.StartRent >= startTime && s.EndRent <= endTime))
                .Sum(s => s.Number) > 0).ToList();
    }

    public static List<RentedItem> GetNoDress(DateOnly date, TimeOnly startTime, TimeOnly endTime)
    {
        return GetWithTime(date, startTime, endTime).Where(r => !r.IsСlothes).ToList();
    }

    public static List<RentedItem> GetСlothes(DateOnly date, TimeOnly startTime, TimeOnly endTime)
    {
        return GetWithTime(date, startTime, endTime).Where(r => r.IsСlothes).ToList();
    }

    public static List<RentedItem> GetKidsСlothes(DateOnly date, TimeOnly startTime, TimeOnly endTime)
    {
        return GetСlothes(date, startTime, endTime).Where(r => r.IsKids).ToList();
    }

    public static RentedItem? GetByID(int ID)
    {
        return ContextDb.GetRentedItems().FirstOrDefault(r => r.ID == ID);
    }

    public static void Update()
    {
        ContextDb.Save();
    }

    #endregion

    #region Properties

    public int ID { get; set; }
    public uint Number { get; set; }
    public bool IsСlothes { get; set; }
    public bool IsKids { get; set; }

    public virtual List<ExecuteableService> Services { get; set; }

    #endregion

    #region Constructors

    public RentedItem()
    {
        Services = new List<ExecuteableService>();
    }

    public RentedItem(string title, string description, uint number, decimal cost, bool isСlothes, bool isKids) : base(
        title,
        description, cost)
    {
        Number = number;
        IsСlothes = isСlothes;
        IsKids = isKids;
        Services = new List<ExecuteableService>();
    }

    #endregion
}