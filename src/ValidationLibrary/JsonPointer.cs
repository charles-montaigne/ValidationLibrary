namespace ValidationLibrary;

/// <summary>
/// Represent a JsonPointer https://datatracker.ietf.org/doc/html/rfc6901
/// </summary>
public sealed class JsonPointer : IProperty
{
    private readonly string _value;

    private JsonPointer(string value)
    {
        _value = value;
    }

    public static IProperty Root => new JsonPointer("#");

    public IProperty WithSubProperty(string propertyName) => new JsonPointer($"{_value}/{Camel(propertyName)}");
    //public IProperty WithSubProperty(string collectionName, int index) => new JsonPointer($"{Value}/{Camel(collectionName)}/{index}");
    //public IProperty WithIndex(int index) => new JsonPointer($"{Value}/{index}");

    private static string Camel(string propertyName)
    {
        return System.Text.Json.JsonNamingPolicy.CamelCase.ConvertName(propertyName);
    }
}