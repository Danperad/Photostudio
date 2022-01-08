using System.ComponentModel.DataAnnotations;

namespace PhotostudioDLL.Entities;

public class EmployeeProfile
{
    #region Properties

    public int ID { get; set; }
    [Required] public virtual Employee Employee { get; set; }
    [Required] [MaxLength(50)] public string Login { get; set; }
    [Required] [MaxLength(64)] public string Password { get; set; }

    #endregion

    #region Constructors

    public EmployeeProfile()
    {
    }

    public EmployeeProfile(string Login, string Password)
    {
        this.Login = Login;
        this.Password = Password;
    }

    #endregion
}