using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;

namespace Kantaiko.Properties.Immutable;

public class ImmutablePropertyCollection : IImmutablePropertyCollection
{
    private readonly IImmutableDictionary<Type, object>? _objects;

    public ImmutablePropertyCollection() { }

    public ImmutablePropertyCollection(IEnumerable<object> objects)
    {
        _objects = objects.ToImmutableDictionary(x => x.GetType(), x => x);
    }

    internal ImmutablePropertyCollection(IImmutableDictionary<Type, object> objects)
    {
        _objects = objects;
    }

    public IEnumerable<object> Objects => _objects?.Values ?? Array.Empty<object>();

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

    public IImmutablePropertyCollection Set<T>(T value) where T : notnull
    {
        ArgumentNullException.ThrowIfNull(value);

        if (_objects is null)
        {
            return new ImmutablePropertyCollection(new object[] { value });
        }

        var objects = _objects.SetItem(typeof(T), value);
        return objects == _objects ? this : new ImmutablePropertyCollection(objects);
    }

    public IImmutablePropertyCollection Remove(Type propertiesType)
    {
        ArgumentNullException.ThrowIfNull(propertiesType);

        if (_objects is null || !_objects.ContainsKey(propertiesType))
        {
            return this;
        }

        var objects = _objects.Remove(propertiesType);
        return new ImmutablePropertyCollection(objects);
    }
}
