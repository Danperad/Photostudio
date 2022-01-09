namespace PhotostudioDLL.Entities;

public class Hall : Costable
{
    #region Methods

    public static void Add(Hall hall)
    {
        ContextDB.Add(hall);
    }

    public static List<Hall> Get()
    {
        return ContextDB.GetHalls();
    }

    public static void Update()
    {
        ContextDB.Save();
    }

    #endregion

    #region Properties

    public int ID { get; set; }
    public virtual List<ServiceProvided> Services { get; set; }

    #endregion

    #region Constructors

    public Hall()
    {
    }

    public Hall(string Title, string Description, decimal Cost) : base(Title, Description, Cost)
    {
    }

    #endregion
}