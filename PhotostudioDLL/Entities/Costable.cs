namespace PhotostudioDLL.Entities;

public abstract class Costable
{
    #region Properties

    public string Title { get; set; }
    public decimal Cost { get; set; }
    public string Description { get; set; }

    #endregion

    protected static bool Check(Costable costable)
    {
        return costable.Cost > 0;
    }
    
    #region Constructors

    protected Costable()
    {
    }

    protected Costable(string title, string description, decimal cost)
    {
        Title = title;
        Cost = cost;
        Description = description;
    }

    #endregion
}