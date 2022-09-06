using System.Diagnostics.CodeAnalysis;

namespace Kantaiko.Properties;

/// <summary>
/// <inheritdoc cref="IPropertyCollection"/>
/// </summary>
public class PropertyCollection : IPropertyCollection
{
    private Dictionary<Type, object>? _objects;

    /// <summary>
    /// Creates an empty <see cref="PropertyCollection"/>.
    /// </summary>
    public PropertyCollection() { }

    /// <summary>
    /// Creates a <see cref="PropertyCollection"/> with the specified properties.
    /// </summary>
    /// <param name="objects">A collection of property instances.</param>
    public PropertyCollection(IEnumerable<object> objects)
    {
        ArgumentNullException.ThrowIfNull(objects);

        // ReSharper disable once ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract
        _objects = objects.Where(x => x is not null).ToDictionary(x => x.GetType(), x => x);
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

    public bool Remove(Type type)
    {
        ArgumentNullException.ThrowIfNull(type);

        return _objects?.Remove(type) ?? false;
    }

    public void Clear()
    {
        _objects?.Clear();
    }
}
