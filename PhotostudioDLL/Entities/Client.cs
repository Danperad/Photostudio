using System.ComponentModel.DataAnnotations;
using Castle.Core.Internal;

namespace PhotostudioDLL.Entity;

public class Client : People
{
    #region Properties

    public int ID { get; set; }
    [Required] public bool IsActive { get; set; }

    #endregion

    #region Constructors

    public Client()
    {
    }

    public Client(string LastName, string FirstName, string PhoneNumber) : base(LastName, FirstName, PhoneNumber)
    {
    }

    public Client(string LastName, string FirstName, string MiddleName, string PhoneNumber) : base(LastName, FirstName,
        MiddleName, PhoneNumber)
    {
    }

    public Client(string LastName, string FirstName, string MiddleName, string PhoneNumber, string EMail) : base(
        LastName, FirstName, MiddleName, PhoneNumber, EMail)
    {
    }

    #endregion

    public static void Add(Client client)
    {
        Check(client);
        ContextDB.Add(client);
    }

    public static List<Client> Get() => ContextDB.GetClients();
}