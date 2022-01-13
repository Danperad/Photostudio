namespace PhotostudioDLL.Entities;

public abstract class Costable
{
    /// <summary>
    ///     Проверка на положительную цену
    /// </summary>
    /// <param name="costable"></param>
    /// <returns></returns>
    protected static bool Check(Costable costable)
    {
        return costable.Cost > 0;
    }

    #region Properties

    public string Title { get; set; }
    public decimal? Cost { get; set; }
    public string Description { get; set; }

    #endregion

    #region Constructors

    protected Costable()
    {
    }

    protected Costable(string title, string description)
    {
        Title = title;
        Description = description;
    }

    protected Costable(string title, string description, decimal cost) : this(title, description)
    {
        Cost = cost;
    }

    #endregion
}