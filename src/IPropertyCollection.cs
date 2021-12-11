namespace Kantaiko.Properties;

public interface IPropertyCollection : IReadOnlyPropertyCollection
{
    T GetOrCreate<T>() where T : notnull, new();

    void Set<T>(T value) where T : notnull;

    bool Remove(Type propertiesType);

    void Clear();
}
