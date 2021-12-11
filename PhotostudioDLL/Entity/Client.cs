namespace PhotostudioDLL.Entity;

public class Client
{
    private static ApplicationContext db = Context.db;
    
    public int ID { get; set; }
    [Required] [MaxLength(50)] public string LastName { get; set; }
    [Required] [MaxLength(50)] public string FirstName { get; set; }
    [MaxLength(50)] public string MiddleName { get; set; }
    [Required] [MaxLength(15)] public string PhoneNumber { get; set; }
    [MaxLength(50)] public string EMail { get; set; }

    public string GetName()
    {
        var temp = $"{LastName} {FirstName.Substring(0, 1)}.";
        if (MiddleName != null) temp += $" {MiddleName.Substring(0, 1)}.";
        return temp;
    }

    public static void Add(Client client)
    {
        db.Client.Add(client);
        db.SaveChanges();
    }

    public static void AddAsync(Client client)
    {
        db.Client.AddAsync(client);
        db.SaveChanges();
    }

    public static void AddRange(params Client[] clients)
    {
        foreach (var client in clients) db.Client.Add(client);
        db.SaveChanges();
    }

    public static void AddRangeAsync(params Client[] clients)
    {
        foreach (var client in clients) db.Client.AddAsync(client);
        db.SaveChanges();
    }

    public static List<Client> Get()
    {
        return db.Client.ToList();
    }
}