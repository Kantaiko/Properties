namespace Kantaiko.Properties.Immutable;

/// <summary>
/// Represents an immutable collection of user-defined properties.
/// </summary>
public interface IImmutablePropertyCollection : IReadOnlyPropertyCollection
{
    /// <summary>
    /// Returns a new collection with the specified instance added.
    /// </summary>
    /// <param name="value">An instance to add.</param>
    /// <typeparam name="T">The type of the properties.</typeparam>
    /// <returns>A new collection with the specified instance added.</returns>
    IImmutablePropertyCollection Set<T>(T value) where T : notnull;

    /// <summary>
    /// Returns a new collection with an instance of the specified type removed.
    /// </summary>
    /// <param name="propertiesType">A type of the properties to remove.</param>
    /// <returns>
    /// A new collection with an instance of the specified type removed
    /// or the same collection if the instance of the specified type is not found.
    /// </returns>
    IImmutablePropertyCollection Remove(Type propertiesType);
}
