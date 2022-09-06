namespace Kantaiko.Properties;

/// <summary>
/// The extensions for <see cref="IPropertyContainer"/>.
/// </summary>
public static class PropertyContainerExtensions
{
    /// <summary>
    /// Invokes a provided function for an instance of the specified type.
    /// <br/>
    /// If no instance of the specified type was found, a new one will be created and added to the collection.
    /// </summary>
    /// <param name="propertyContainer">An object that contains properties.</param>
    /// <param name="configureDelegate">A function that accepts an instance of the specified type.</param>
    /// <typeparam name="T">The type of the properties.</typeparam>
    public static void Configure<T>(this IPropertyContainer propertyContainer, Action<T> configureDelegate)
        where T : notnull, new()
    {
        ArgumentNullException.ThrowIfNull(propertyContainer);

        propertyContainer.Properties.Configure(configureDelegate);
    }

    /// <summary>
    /// Replaces an instance of the specified type with a new one applying the provided function.
    /// <br/>
    /// If no instance of the specified type was found, a new one will be created and added to the collection.
    /// </summary>
    /// <param name="propertyContainer">An object that contains properties.</param>
    /// <param name="updateDelegate">A function that accepts an instance of the specified type and returns a new one.</param>
    /// <typeparam name="T">The type of the properties.</typeparam>
    public static void Update<T>(this IPropertyContainer propertyContainer, Func<T, T> updateDelegate)
        where T : notnull, new()
    {
        ArgumentNullException.ThrowIfNull(propertyContainer);

        propertyContainer.Properties.Update(updateDelegate);
    }
}
