namespace Kantaiko.Properties;

/// <summary>
/// Represents a collection of user-defined properties.
/// </summary>
public interface IPropertyCollection : IReadOnlyPropertyCollection
{
    /// <summary>
    /// Gets an existing instance of the specified type or creates and adds a new one.
    /// </summary>
    /// <typeparam name="T">The type of the properties. Must have a parameterless constructor.</typeparam>
    /// <returns>An instance of the specified type.</returns>
    T GetOrCreate<T>() where T : notnull, new();

    /// <summary>
    /// Adds or replaces an instance of the specified type.
    /// </summary>
    /// <param name="value">An instance of the property class.</param>
    /// <typeparam name="T">The type of the properties.</typeparam>
    void Set<T>(T value) where T : notnull;

    /// <summary>
    /// Removes an instance of the specified type.
    /// </summary>
    /// <param name="type">A type of the properties.</param>
    /// <returns><see langword="true"/> if an instance was removed; otherwise, <see langword="false"/>.</returns>
    bool Remove(Type type);

    /// <summary>
    /// Clears the collection.
    /// </summary>
    void Clear();
}
