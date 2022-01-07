using PhotostudioDLL.Entity;

namespace PhotostudioDLL.Exception;

public class ServiceProvidedExeption : System.Exception
{
    public ServiceProvided ServiceProvided { get; private set; }
    public override string Message { get; }

    #region Constructors

    public ServiceProvidedExeption()
    {
        Message = "ServiceProvidedError";
    }

    public ServiceProvidedExeption(string message)
    {
        Message = message;
    }
    
    public ServiceProvidedExeption(string message,ServiceProvided serviceProvided) : base(message)
    {
        ServiceProvided = serviceProvided;
    }
    
    #endregion
}