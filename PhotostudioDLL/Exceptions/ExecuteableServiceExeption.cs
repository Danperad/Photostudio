using PhotostudioDLL.Entities;

namespace PhotostudioDLL.Exceptions;

public class ExecuteableServiceExeption : Exception
{
    public ExecuteableService ExecuteableService { get; }
    public override string Message { get; }

    #region Constructors

    public ExecuteableServiceExeption()
    {
        Message = "ExecuteableServiceError";
    }

    public ExecuteableServiceExeption(string message)
    {
        Message = message;
    }

    public ExecuteableServiceExeption(string message, ExecuteableService executeableService) : base(message)
    {
        ExecuteableService = executeableService;
    }

    #endregion
}