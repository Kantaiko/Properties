namespace Kantaiko.Properties;

public class PropertiesBase<T> where T : PropertiesBase<T>, new()
{
    public static T? Of(IReadOnlyPropertyContainer propertyContainer)
    {
        ArgumentNullException.ThrowIfNull(propertyContainer);

        return propertyContainer.Properties.Get<T>();
    }
}
