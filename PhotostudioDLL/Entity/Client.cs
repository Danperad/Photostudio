
using System.ComponentModel.DataAnnotations;

namespace PhotostudioDLL.Entity;

public class Client
{
    public int ID { get; set; }
    [Required] [MaxLength(50)] public string LastName { get; set; }
    [Required] [MaxLength(50)] public string FirstName { get; set; }
    [MaxLength(50)] public string? MiddleName { get; set; }
    [Required] [MaxLength(15)] public string PhoneNumber { get; set; }
    [MaxLength(50)] public string? EMail { get; set; }
    [Required] public bool IsActive { get; set; }

    public Client(){}

    public Client(string LastName, string FirstName, string PhoneNumber)
    {
        this.LastName = LastName;
        this.FirstName = FirstName;
        this.PhoneNumber = PhoneNumber;
    }
    
    public string GetName()
    {
        var temp = $"{LastName} {FirstName.Substring(0, 1)}.";
        if (MiddleName != null) temp += $" {MiddleName.Substring(0, 1)}.";
        return temp;
    }
}