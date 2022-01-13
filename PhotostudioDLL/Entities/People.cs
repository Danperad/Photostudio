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

    protected People()
    {
    }

    protected People(string lastName, string firstName, string phoneNumber)
    {
        LastName = lastName;
        FirstName = firstName;
        PhoneNumber = phoneNumber;
    }

    public People(string lastName, string firstName, string middleName, string phoneNumber) : this(lastName, firstName,
        phoneNumber)
    {
        MiddleName = middleName;
    }

    public People(string lastName, string firstName, string middleName, string phoneNumber, string eMail) : this(
        lastName, firstName, middleName, phoneNumber)
    {
        EMail = eMail;
    }

    #endregion
}