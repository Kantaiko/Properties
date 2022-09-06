using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;

namespace Kantaiko.Properties.Immutable;

/// <summary>
/// <inheritdoc cref="IImmutablePropertyCollection"/>
/// </summary>
public class ImmutablePropertyCollection : IImmutablePropertyCollection
{
    private readonly IImmutableDictionary<Type, object>? _objects;

    /// <summary>
    /// Creates an empty <see cref="ImmutablePropertyCollection"/>.
    /// </summary>
    public ImmutablePropertyCollection() { }

    /// <summary>
    /// Creates a new <see cref="ImmutablePropertyCollection"/> with the specified properties.
    /// </summary>
    /// <param name="objects">A collection of property instances.</param>
    public ImmutablePropertyCollection(IEnumerable<object> objects)
    {
        ArgumentNullException.ThrowIfNull(objects);

        // ReSharper disable once ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract
        _objects = objects.Where(x => x is not null).ToImmutableDictionary(x => x.GetType(), x => x);
    }

    private ImmutablePropertyCollection(IImmutableDictionary<Type, object> objects)
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

    /// <summary>
    /// An empty <see cref="ImmutablePropertyCollection"/>.
    /// <br/>
    /// Can be used to create new immutable collections by calling <see cref="Set{T}"/>
    /// without creating a useless empty collection.
    /// </summary>
    public static IImmutablePropertyCollection Empty { get; } = new ImmutablePropertyCollection();
}
