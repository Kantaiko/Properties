using System.Diagnostics.CodeAnalysis;

namespace Kantaiko.Properties;

public class PropertyCollection : IPropertyCollection
{
    private Dictionary<Type, object>? _objects;

    public PropertyCollection() { }

    public PropertyCollection(IEnumerable<object> objects)
    {
        _objects = objects.ToDictionary(x => x.GetType(), x => x);
    }

    public IEnumerable<object> Objects => (IEnumerable<object>?) _objects?.Values ?? Array.Empty<object>();

    public T GetOrCreate<T>() where T : notnull, new()
    {
        _objects ??= new Dictionary<Type, object>();

        if (!_objects.TryGetValue(typeof(T), out var value))
        {
            _objects[typeof(T)] = value = new T();
        }

        return (T) value;
    }

    public bool TryGet<T>([NotNullWhen(true)] out T? properties) where T : notnull
    {
        if (_objects is null || !_objects.TryGetValue(typeof(T), out var value))
        {
            properties = default;
            return false;
        }

        properties = (T) value;
        return true;
    }

    public void Set<T>(T value) where T : notnull
    {
        ArgumentNullException.ThrowIfNull(value);

        _objects ??= new Dictionary<Type, object>();
        _objects[typeof(T)] = value;
    }

    public bool Remove(Type propertiesType)
    {
        ArgumentNullException.ThrowIfNull(propertiesType);

        return _objects?.Remove(propertiesType) ?? false;
    }

    public void Clear()
    {
        _objects?.Clear();
    }
}
