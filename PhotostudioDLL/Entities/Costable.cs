namespace PhotostudioDLL.Entities;

public abstract class Costable
{
    #region Properties

    public string Title { get; set; }
    public decimal Cost { get; set; }
    public string Description { get; set; }

    #endregion

    #region Constructors

    protected Costable()
    {
    }

    protected Costable(string Title, string Description, decimal Cost)
    {
        this.Title = Title;
        this.Cost = Cost;
        this.Description = Description;
    }

    #endregion
}