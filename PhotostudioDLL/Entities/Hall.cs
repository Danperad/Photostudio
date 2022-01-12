namespace PhotostudioDLL.Entities;

public class Hall : Costable
{
    #region Methods

    public static List<Hall> Get()
    {
        return ContextDb.GetHalls();
    }

    public static void Update()
    {
        ContextDb.Save();
    }

    #endregion

    #region Properties

    public int ID { get; set; }
    public virtual List<ExecuteableService> Services { get; set; }

    #endregion

    #region Constructors

    public Hall()
    {
        Services = new List<ExecuteableService>();
    }

    public Hall(string title, string description, decimal cost) : base(title, description, cost)
    {
        Services = new List<ExecuteableService>();
    }

    #endregion
}