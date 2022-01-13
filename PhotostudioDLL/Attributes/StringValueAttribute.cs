namespace PhotostudioDLL.Attributes;

/// <summary>
///     Атрибут для хранения в свойстве или поле информации
/// </summary>
public class StringValueAttribute : Attribute
{
    public StringValueAttribute(string? value)
    {
        StringValue = value;
    }

    public string? StringValue { get; protected set; }
}