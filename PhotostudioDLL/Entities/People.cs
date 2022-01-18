using Castle.Core.Internal;

namespace PhotostudioDLL.Entities;

public abstract class People
{
    /// <summary>
    ///     Проверка на корректность Человека
    /// </summary>
    /// <param name="people"></param>
    /// <returns></returns>
    protected static bool Check(People people)
    {
        return !(people.FirstName.IsNullOrEmpty() && people.LastName.IsNullOrEmpty() &&
                 people.PhoneNumber.IsNullOrEmpty());
    }

    #region Properties

    public string LastName { get; set; }
    public string FirstName { get; set; }
    public string? MiddleName { get; set; }
    public string PhoneNumber { get; set; }
    public string? EMail { get; set; }

    public virtual string FullName
    {
        get
        {
            var temp = $"{LastName} {FirstName[..1]}.";
            if (MiddleName != null) temp += $" {MiddleName[..1]}.";
            return temp;
        }
    }

    #endregion

    #region Constructors

    protected People()
    {
    }

    protected People(string lastName, string firstName, string phoneNumber)
    {
        LastName = lastName;
        FirstName = firstName;
        PhoneNumber = phoneNumber;
    }

    protected People(string lastName, string firstName, string middleName, string phoneNumber) : this(lastName, firstName,
        phoneNumber)
    {
        MiddleName = middleName;
    }

    protected People(string lastName, string firstName, string middleName, string phoneNumber, string eMail) : this(
        lastName, firstName, middleName, phoneNumber)
    {
        EMail = eMail;
    }

    #endregion
}