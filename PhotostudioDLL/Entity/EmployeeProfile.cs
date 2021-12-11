namespace PhotostudioDLL.Entity;

public class EmployeeProfile
{
    public int ID { get; set; }

    [MaxLength(50)] public string Login { get; set; }
    [MaxLength(64)] public string Password { get; set; }
    public Employee Employee { get; set; }
}