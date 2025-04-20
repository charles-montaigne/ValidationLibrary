namespace ValidationLibrary;

/// <summary>
/// Represent a JsonPointer https://datatracker.ietf.org/doc/html/rfc6901
/// </summary>
public sealed class JsonPointer : IProperty
{
    public string Value { get; }

    private JsonPointer(string value)
    {
        Value = value;
    }

    private static string Camel(string propertyName)
    {
        return System.Text.Json.JsonNamingPolicy.CamelCase.ConvertName(propertyName);
    }

    public static JsonPointer OfRootElement => new JsonPointer(rootElement);
    private static readonly string rootElement = "#";

    public IProperty WithSubProperty(string propertyName) => new JsonPointer($"{Value}/{Camel(propertyName)}");
    //public IProperty WithSubProperty(string collectionName, int index) => new JsonPointer($"{Value}/{Camel(collectionName)}/{index}");
    //public IProperty WithIndex(int index) => new JsonPointer($"{Value}/{index}");
}
