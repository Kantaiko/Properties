namespace Kantaiko.Properties;

/// <summary>
/// The extensions for <see cref="IPropertyCollection"/>.
/// </summary>
public static class PropertyCollectionExtensions
{
    /// <summary>
    /// Invokes a provided function for an instance of the specified type.
    /// <br/>
    /// If no instance of the specified type was found, a new one will be created and added to the collection.
    /// </summary>
    /// <param name="propertyCollection">A property collection.</param>
    /// <param name="configureDelegate">A function that accepts an instance of the specified type.</param>
    /// <typeparam name="T">The type of the properties.</typeparam>
    public static void Configure<T>(this IPropertyCollection propertyCollection, Action<T> configureDelegate)
        where T : notnull, new()
    {
        ArgumentNullException.ThrowIfNull(propertyCollection);
        ArgumentNullException.ThrowIfNull(configureDelegate);

        configureDelegate.Invoke(propertyCollection.GetOrCreate<T>());
    }

    /// <summary>
    /// Replaces an instance of the specified type with a new one applying the provided function.
    /// <br/>
    /// If no instance of the specified type was found, a new one will be created and added to the collection.
    /// </summary>
    /// <param name="propertyCollection">A property collection.</param>
    /// <param name="updateDelegate">
    /// A function that accepts an instance of the specified type and returns a new one.
    /// </param>
    /// <typeparam name="T">The type of the properties.</typeparam>
    public static void Update<T>(this IPropertyCollection propertyCollection, Func<T, T> updateDelegate)
        where T : notnull, new()
    {
        ArgumentNullException.ThrowIfNull(propertyCollection);
        ArgumentNullException.ThrowIfNull(updateDelegate);

        var value = updateDelegate.Invoke(propertyCollection.GetOrCreate<T>());
        propertyCollection.Set(value);
    }

    /// <summary>
    /// Removes an instance of the specified type from the collection.
    /// </summary>
    /// <param name="propertyCollection">A property collection.</param>
    /// <typeparam name="T">The type of the properties.</typeparam>
    /// <returns><see langword="true"/> if an instance was removed; otherwise, <see langword="false"/>.</returns>
    public static bool Remove<T>(this IPropertyCollection propertyCollection) where T : notnull
    {
        ArgumentNullException.ThrowIfNull(propertyCollection);

        return propertyCollection.Remove(typeof(T));
    }
}
