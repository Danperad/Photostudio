using System.ComponentModel.DataAnnotations;

namespace PhotostudioDLL.Entity;

public class EmployeeProfile
{
    public int ID { get; set; }

    [Required] [MaxLength(50)] public string Login { get; set; }
    [Required] [MaxLength(64)] public string Password { get; set; }
    [Required] public virtual Employee Employee { get; set; }
    public EmployeeProfile(){}

    public EmployeeProfile(string Login, string Password)
    {
        this.Login = Login;
        this.Password = Password;
    }
}