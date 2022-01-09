namespace PhotostudioDLL.Entities;

public class Client : People
{
    #region Methods

    public static void Add(Client client)
    {
        Check(client);
        ContextDB.Add(client);
    }

    public static List<Client> Get()
    {
        return ContextDB.GetClients();
    }

    public static void Update()
    {
        ContextDB.Save();
    }

    #endregion

    #region Properties

    public int ID { get; set; }
    public virtual List<Order> Orders { get; set; }
    public virtual List<Contract> Contracts { get; set; }
    
    public string? Company { get; set; }

    #endregion

    #region Constructors

    public Client()
    {
        Orders = new List<Order>();
        Contracts = new List<Contract>();
    }

    public Client(string LastName, string FirstName, string PhoneNumber) : base(LastName, FirstName, PhoneNumber)
    {
        Orders = new List<Order>();
        Contracts = new List<Contract>();
    }

    public Client(string LastName, string FirstName, string PhoneNumber, string MiddleName) : base(LastName, FirstName,
        MiddleName, PhoneNumber)
    {
        Orders = new List<Order>();
        Contracts = new List<Contract>();
    }

    public Client(string LastName, string FirstName, string PhoneNumber, string MiddleName, string EMail) : base(
        LastName, FirstName, MiddleName, PhoneNumber, EMail)
    {
        Orders = new List<Order>();
        Contracts = new List<Contract>();
    }

    #endregion
}