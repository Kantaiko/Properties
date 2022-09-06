using System.Diagnostics.CodeAnalysis;

namespace Kantaiko.Properties;

/// <summary>
/// Represents a readonly collection of user-defined properties.
/// </summary>
public interface IReadOnlyPropertyCollection
{
    /// <summary>
    /// Gets an instance of the specified type from the collection.
    /// </summary>
    /// <param name="properties">A variable to populate with the properties.</param>
    /// <typeparam name="T">The type of the properties.</typeparam>
    /// <returns>
    /// <see langword="true"/> if an instance of the specified type was found; otherwise, <see langword="false"/>.
    /// </returns>
    bool TryGet<T>([NotNullWhen(true)] out T? properties) where T : notnull;

    /// <summary>
    /// The enumerable of all property instances in the collection.
    /// </summary>
    IEnumerable<object> Objects { get; }
}
