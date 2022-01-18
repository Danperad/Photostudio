namespace PhotostudioDLL.Entities.Interfaces;

public interface ICostable
{
    /// <summary>
    ///     Проверка на положительную цену
    /// </summary>
    /// <param name="costable"></param>
    /// <returns></returns>
    protected static bool Check(ICostable costable)
    {
        return costable.Cost > 0;
    }

    #region Properties

    string Title { get; set; }
    decimal? Cost { get; set; }
    string Description { get; set; }

    #endregion
}