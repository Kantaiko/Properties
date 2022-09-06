namespace Kantaiko.Properties;

/// <summary>
/// The base class for property classes.
/// </summary>
/// <typeparam name="T">The type of the properties.</typeparam>
public abstract class PropertyClass<T> where T : PropertyClass<T>
{
    /// <summary>
    /// Gets an instance of the properties contained in the specified object.
    /// </summary>
    /// <param name="propertyContainer">An object that contains the properties.</param>
    /// <returns>
    /// An instance of the properties or null if the object does not contain the properties of this type.
    /// </returns>
    public static T? Of(IReadOnlyPropertyContainer propertyContainer)
    {
        ArgumentNullException.ThrowIfNull(propertyContainer);

        return propertyContainer.Properties.Get<T>();
    }
}
