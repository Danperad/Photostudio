using System.Text;
using PhotostudioDLL.Entities;
using PhotostudioDLL.Exceptions;

namespace PhotostudioDLL;

/// <summary>
/// Класс для работы с базой данных
/// </summary>
internal static class ContextDb
{
    private static ApplicationContext Db { get; set; }

    internal static void AddDb(ApplicationContext context)
    {
        Db = context;
    }

    public static void Save()
    {
        Db.SaveChanges();
    }

    #region Client

    public static void Add(Client client)
    {
        Db.Client.Add(client);
        Db.SaveChanges();
    }

    public static List<Client> GetClients()
    {
        return Db.Client.ToList();
    }

    #endregion

    #region Contract

    public static List<Contract> GetContracts()
    {
        return Db.Contract.ToList();
    }

    #endregion

    #region Employee

    public static void Add(Employee employee)
    {
        Db.Employee.Add(employee);
        Db.SaveChanges();
    }

    public static List<Employee> GetEmployees()
    {
        return Db.Employee.ToList();
    }

    public static Employee? GetAuth(string login, string pass)
    {
        return Db.Employee.FirstOrDefault(d => d.Profile.Login == login && d.Profile.Password == pass);
    }

    public static Employee? GetEmployeeById(int id)
    {
        return Db.Employee.FirstOrDefault(employee => employee.ID == id);
    }

    #endregion

    #region Equipment

    public static void Add(Equipment equipment)
    {
        Db.Equipment.Add(equipment);
        Db.SaveChanges();
    }

    public static List<Equipment> GetEquipments()
    {
        return Db.Equipment.ToList();
    }

    #endregion

    #region Hall

    public static List<Hall> GetHalls()
    {
        return Db.Hall.ToList();
    }

    #endregion

    #region Inventory

    public static void Add(Inventory inventory)
    {
        Db.Inventory.Add(inventory);
        Db.SaveChanges();
    }

    public static List<Inventory> GetInventories()
    {
        return Db.Inventory.ToList();
    }

    #endregion

    #region Order

    public static void Add(Order order)
    {
        if (CheckOrder(order)) Db.Order.Add(order);
        Db.SaveChanges();
    }

    public static List<Order> GetOrders()
    {
        return Db.Order.ToList();
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

    public static string GetServiceName(List<ExecuteableService> services)
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
        Db.RentedItem.Add(rentedItem);
        Db.SaveChanges();
    }

    public static List<RentedItem> GetRentedItems()
    {
        return Db.RentedItem.ToList();
    }

    #endregion

    #region Role

    public static void Add(Role role)
    {
        Db.Role.Add(role);
        Db.SaveChanges();
    }

    public static List<Role> GetRoles()
    {
        return Db.Role.ToList();
    }

    public static Role GetRoleByID(int id)
    {
        return Db.Role.Where(d => d.ID == id).FirstOrDefault();
    }

    #endregion

    #region Services

    public static List<Service> GetServices()
    {
        Console.WriteLine(Db.Service.GetType().Name);
        return Db.Service.ToList();
    }

    #endregion

    #region ExecuteableService

    public static List<ExecuteableService> GetExecuteableService()
    {
        return Db.ExecuteableService.ToList();
    }

    #endregion
}