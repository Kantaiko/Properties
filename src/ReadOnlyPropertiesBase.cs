namespace Kantaiko.Properties;

public record ReadOnlyPropertiesBase<T> where T : ReadOnlyPropertiesBase<T>
{
    public static T? Of(IReadOnlyPropertyContainer propertyContainer)
    {
        ArgumentNullException.ThrowIfNull(propertyContainer);

        return propertyContainer.Properties.Get<T>();
    }
}
