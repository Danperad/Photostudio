using System.ComponentModel.DataAnnotations;
using Castle.Core.Internal;

namespace PhotostudioDLL.Entities;

public abstract class People
{
    protected static void Check(People people)
    {
        if (people.FirstName.IsNullOrEmpty())
            throw new Exception("FirstNameError");
        if (people.LastName.IsNullOrEmpty())
            throw new Exception("LastNameError");
        if (people.PhoneNumber.IsNullOrEmpty())
            throw new Exception("PhoneNumberError");
    }

    #region Properties

    [Required] [MaxLength(50)] public string LastName { get; set; }
    [Required] [MaxLength(50)] public string FirstName { get; set; }
    [MaxLength(50)] public string? MiddleName { get; set; }
    [Required] [MaxLength(15)] public string PhoneNumber { get; set; }
    [MaxLength(50)] public string? EMail { get; set; }

    public string FullName
    {
        get
        {
            var temp = $"{LastName} {FirstName.Substring(0, 1)}.";
            if (MiddleName != null) temp += $" {MiddleName.Substring(0, 1)}.";
            return temp;
        }
    }

    #endregion


    #region Constructors

    public People()
    {
    }

    public People(string LastName, string FirstName, string PhoneNumber)
    {
        this.LastName = LastName;
        this.FirstName = FirstName;
        this.PhoneNumber = PhoneNumber;
    }

    public People(string LastName, string FirstName, string MiddleName, string PhoneNumber)
    {
        this.LastName = LastName;
        this.FirstName = FirstName;
        this.MiddleName = MiddleName;
        this.PhoneNumber = PhoneNumber;
    }

    public People(string LastName, string FirstName, string MiddleName, string PhoneNumber, string EMail)
    {
        this.LastName = LastName;
        this.FirstName = FirstName;
        this.MiddleName = MiddleName;
        this.PhoneNumber = PhoneNumber;
        this.EMail = EMail;
    }

    #endregion
}