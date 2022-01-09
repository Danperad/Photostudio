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

    public virtual List<ServiceProvided> Services { get; set; }

    #endregion

    #region Constructors

    public RentedItem()
    {
        Services = new List<ServiceProvided>();
    }

    public RentedItem(string Title, string Description, uint Number, decimal Cost, bool IsСlothes) : base(Title,
        Description, Cost)
    {
        this.Number = Number;
        this.IsСlothes = IsСlothes;
        Services = new List<ServiceProvided>();
    }

    #endregion
}