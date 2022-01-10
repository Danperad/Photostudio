namespace PhotostudioDLL.Entities;

public class RentedItem : Costable
{
    #region Methods

    public static void Add(RentedItem rentedItem)
    {
        ContextDB.Add(rentedItem);
    }

    public static List<RentedItem> Get()
    {
        return ContextDB.GetRentedItems();
    }

    private static List<RentedItem> GetWithTime(DateOnly date, TimeOnly startTime, TimeOnly endTime)
    {
        return ContextDB.GetRentedItems()
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
        return ContextDB.GetRentedItems().FirstOrDefault(r => r.ID == ID);
    }

    public static void Update()
    {
        ContextDB.Save();
    }

    #endregion

    #region Properties

    public int ID { get; set; }
    public uint Number { get; set; }
    public bool IsСlothes { get; set; }
    public bool IsKids { get; set; }

    public virtual List<ServiceProvided> Services { get; set; }

    #endregion

    #region Constructors

    public RentedItem()
    {
        Services = new List<ServiceProvided>();
    }

    public RentedItem(string Title, string Description, uint Number, decimal Cost, bool IsСlothes, bool IsKids) : base(
        Title,
        Description, Cost)
    {
        this.Number = Number;
        this.IsСlothes = IsСlothes;
        this.IsKids = IsKids;
        Services = new List<ServiceProvided>();
    }

    #endregion
}