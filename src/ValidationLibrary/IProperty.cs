namespace ValidationLibrary;

public interface IProperty
{
    public IProperty WithSubProperty(string propertyName);
    //public JsonPointer WithSubProperty(string collectionName, int index);
    //public JsonPointer WithIndex(int index);
}
