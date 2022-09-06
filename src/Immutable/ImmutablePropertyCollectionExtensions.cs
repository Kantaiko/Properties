namespace Kantaiko.Properties.Immutable;

/// <summary>
/// The extensions for <see cref="IImmutablePropertyCollection"/>.
/// </summary>
public static class ImmutablePropertyCollectionExtensions
{
    /// <summary>
    /// Returns a new collection with an instance of the specified type removed.
    /// </summary>
    /// <param name="propertyCollection">An immutable property collection.</param>
    /// <typeparam name="T">The type of the properties to remove.</typeparam>
    /// <returns>
    /// A new collection with an instance of the specified type removed
    /// or the same collection if the instance of the specified type is not found.
    /// </returns>
    public static IImmutablePropertyCollection Remove<T>(this IImmutablePropertyCollection propertyCollection)
        where T : notnull
    {
        ArgumentNullException.ThrowIfNull(propertyCollection);

        return propertyCollection.Remove(typeof(T));
    }

    /// <summary>
    /// Creates a new collection with an instance of the specified type updated applying the specified update function.
    /// <br/>
    /// If no instance of the specified type was found, a new instance is created and passed to the update function.
    /// </summary>
    /// <param name="propertyCollection">An immutable property collection.</param>
    /// <param name="updateDelegate">
    /// A function that accepts an instance of the specified type and returns a new one.
    /// </param>
    /// <typeparam name="T">The type of the properties.</typeparam>
    /// <returns>A new collection with an instance of the specified type updated.</returns>
    public static IImmutablePropertyCollection Update<T>(this IImmutablePropertyCollection propertyCollection,
        Func<T, T> updateDelegate)
        where T : notnull, new()
    {
        ArgumentNullException.ThrowIfNull(updateDelegate);

        if (!propertyCollection.TryGet<T>(out var properties))
        {
            properties = new T();
        }

        return propertyCollection.Set(updateDelegate(properties));
    }
}
