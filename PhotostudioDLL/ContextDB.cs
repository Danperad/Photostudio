using System.Text;
using PhotostudioDLL.Entities;
using PhotostudioDLL.Exceptions;

namespace PhotostudioDLL;

internal static class ContextDB
{
    private static ApplicationContext db { get; set; }

    internal static void AddDB(ApplicationContext dbb)
    {
        db = dbb;
    }

    public static void Save()
    {
        db.SaveChanges();
    }

    #region Client

    public static void Add(Client client)
    {
        db.Client.Add(client);
        db.SaveChanges();
    }

    public static List<Client> GetClients()
    {
        return db.Client.ToList();
    }

    public static void ChangeActive(int ID)
    {
        var temp = db.Client.Where(c => c.ID == ID).FirstOrDefault()!;
        temp.IsActive = !temp.IsActive;
        db.SaveChanges();
    }

    #endregion

    #region Contract

    public static void Add(Contract contract)
    {
        db.Contract.Add(contract);
        db.SaveChanges();
    }

    public static List<Contract> GetContracts()
    {
        return db.Contract.ToList();
    }

    #endregion

    #region Employee

    public static void Add(Employee employee)
    {
        db.Employee.Add(employee);
        db.SaveChanges();
    }

    public static List<Employee> GetEmployees()
    {
        return db.Employee.ToList();
    }

    public static Employee GetAuth(string login, string pass)
    {
        return db.Employee.Where(d => d.Profile.Login == login && d.Profile.Password == pass).FirstOrDefault();
    }

    public static Employee GetEmployeeByID(int ID)
    {
        return db.Employee.Where(employee => employee.ID == ID).FirstOrDefault();
    }

    #endregion

    #region Equipment

    public static void Add(Equipment equipment)
    {
        db.Equipment.Add(equipment);
        db.SaveChanges();
    }

    public static List<Equipment> GetEquipments()
    {
        return db.Equipment.ToList();
    }

    #endregion

    #region Hall

    public static void Add(Hall hall)
    {
        db.Hall.Add(hall);
        db.SaveChanges();
    }

    public static List<Hall> GetHalls()
    {
        return db.Hall.ToList();
    }

    #endregion

    #region Inventory

    public static void Add(Inventory inventory)
    {
        db.Inventory.Add(inventory);
        db.SaveChanges();
    }

    public static List<Inventory> GetInventories()
    {
        return db.Inventory.ToList();
    }

    #endregion

    #region Order

    public static void Add(Order order)
    {
        if (CheckOrder(order)) db.Order.Add(order);
        db.SaveChanges();
    }

    public static List<Order> GetOrders()
    {
        return db.Order.ToList();
    }

    private static bool CheckOrder(Order order)
    {
        if (order.Contract.StartDate < DateOnly.FromDateTime(order.DateTime.Date))
            throw new OrderDateException("Не соответствие дат в контракте и заявке", order);
        return true;
    }

    public class FullOrder
    {
        public int ID { get; set; }
        public int Contract { get; set; }
        public string Client { get; set; }
        public DateTime DateTime { get; set; }
        public string Status { get; set; }
        public string Services { get; set; }
    }

    public static string GetServiceName(List<ServiceProvided> services)
    {
        var temp = new StringBuilder();
        foreach (var provided in services) temp.Append($"{provided.Service.Title}, ");

        temp.Remove(temp.Length - 2, 2);
        return temp.ToString();
    }

    #endregion

    #region RentedItem

    public static void Add(RentedItem rentedItem)
    {
        db.RentedItem.Add(rentedItem);
        db.SaveChanges();
    }

    public static List<RentedItem> GetRentedItems()
    {
        return db.RentedItem.ToList();
    }

    #endregion

    #region Role

    public static void Add(Role role)
    {
        db.Role.Add(role);
        db.SaveChanges();
    }

    public static List<Role> GetRoles()
    {
        return db.Role.ToList();
    }

    public static Role GetRoleByID(int id)
    {
        return db.Role.Where(d => d.ID == id).FirstOrDefault();
    }

    #endregion

    #region Services

    public static void Add(Service service)
    {
        db.Service.Add(service);
        db.SaveChanges();
    }

    public static List<Service> GetServices()
    {
        Console.WriteLine(db.Service.GetType().Name);
        return db.Service.ToList();
    }

    #endregion

    #region ServiceProvided

    public static void Add(ServiceProvided serviceProvided)
    {
        db.ServiceProvided.Add(serviceProvided);
        db.SaveChanges();
    }

    public static List<ServiceProvided> GetServiceProvideds()
    {
        return db.ServiceProvided.ToList();
    }

    #endregion
}