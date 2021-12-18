namespace PhotostudioDLL.Entity;

public class Client
{
    public int ID { get; set; }
    [Required] [MaxLength(50)] public string LastName { get; set; }
    [Required] [MaxLength(50)] public string FirstName { get; set; }
    [MaxLength(50)] public string? MiddleName { get; set; }
    [Required] [MaxLength(15)] public string PhoneNumber { get; set; }
    [MaxLength(50)] public string? EMail { get; set; }

    public string GetName()
    {
        var temp = $"{LastName} {FirstName.Substring(0, 1)}.";
        if (MiddleName != null) temp += $" {MiddleName.Substring(0, 1)}.";
        return temp;
    }
}