using Kantaiko.Properties.Immutable;

namespace Kantaiko.Properties;

/// <summary>
/// The extensions for <see cref="IReadOnlyPropertyCollection"/>.
/// </summary>
public static class ReadOnlyPropertyCollectionExtensions
{
    /// <summary>
    /// Gets an instance of the specified type.
    /// </summary>
    /// <param name="propertyCollection">A collection of properties.</param>
    /// <typeparam name="T">The type of the property.</typeparam>
    /// <returns>An instance of the specified type or default if no instance was found in the collection.</returns>
    public static T? Get<T>(this IReadOnlyPropertyCollection propertyCollection) where T : notnull
    {
        return propertyCollection.TryGet<T>(out var properties) ? properties : default;
    }

    /// <summary>
    /// Converts the property collection to an immutable property collection.
    /// <br/>
    /// If the collection is already immutable, it will be returned as is.
    /// </summary>
    /// <param name="propertyCollection">The property collection.</param>
    /// <returns>An immutable property collection.</returns>
    public static IImmutablePropertyCollection ToImmutable(this IReadOnlyPropertyCollection propertyCollection)
    {
        if (propertyCollection is IImmutablePropertyCollection immutablePropertyCollection)
        {
            return immutablePropertyCollection;
        }

        return new ImmutablePropertyCollection(propertyCollection.Objects);
    }
}
