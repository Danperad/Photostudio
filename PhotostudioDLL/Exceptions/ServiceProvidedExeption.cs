using PhotostudioDLL.Entities;

namespace PhotostudioDLL.Exceptions;

public class ServiceProvidedExeption : Exception
{
    public ServiceProvided ServiceProvided { get; }
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

    public ServiceProvidedExeption(string message, ServiceProvided serviceProvided) : base(message)
    {
        ServiceProvided = serviceProvided;
    }

    #endregion
}