using Castle.Core.Internal;

namespace PhotostudioDLL.Entities;

public class Client : People
{
    #region Methods

    /// <summary>
    ///     Добавление нового клиента в Бд
    /// </summary>
    /// <param name="client"></param>
    /// <returns></returns>
    public static bool Add(Client client)
    {
        if (!Check(client)) return false;
        ContextDb.Add(client);
        return true;
    }

    /// <summary>
    ///     Получение списка клиентов
    /// </summary>
    /// <returns></returns>
    public static List<Client> Get()
    {
        return ContextDb.GetClients();
    }

    public static void Update()
    {
        ContextDb.Save();
    }

    #endregion

    #region Properties

    public int ID { get; set; }
    public string? Company { get; set; }
    public virtual List<Order> Orders { get; set; }
    public virtual List<Contract> Contracts { get; set; }

    public override string FullName => Company.IsNullOrEmpty() ? base.FullName : Company!;

    #endregion

    #region Constructors

    public Client()
    {
        Orders = new List<Order>();
        Contracts = new List<Contract>();
    }

    public Client(string lastName, string firstName, string phoneNumber) : base(lastName, firstName, phoneNumber)
    {
        Orders = new List<Order>();
        Contracts = new List<Contract>();
    }

    public Client(string LastName, string firstName, string phoneNumber, string middleName) : base(LastName, firstName,
        middleName, phoneNumber)
    {
        Orders = new List<Order>();
        Contracts = new List<Contract>();
    }

    public Client(string LastName, string firstName, string phoneNumber, string middleName, string EMail) : base(
        LastName, firstName, middleName, phoneNumber, EMail)
    {
        Orders = new List<Order>();
        Contracts = new List<Contract>();
    }

    #endregion
}