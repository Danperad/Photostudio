using PhotostudioDLL.Entity.Interface;

namespace PhotostudioDLL.Exception;

public class MoneyException : System.Exception
{
    public MoneyException(string message, ICostable costable) : base(message)
    {
        Title = costable.GetTitle();
    }

    public string Title { get; }
}