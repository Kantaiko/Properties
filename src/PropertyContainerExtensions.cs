namespace Kantaiko.Properties;

public static class PropertyContainerExtensions
{
    public static void Update<T>(this IPropertyContainer propertyContainer, Func<T, T> updateDelegate)
        where T : notnull, new()
    {
        ArgumentNullException.ThrowIfNull(propertyContainer);

        propertyContainer.Properties.Update(updateDelegate);
    }

    public static void Configure<T>(this IPropertyContainer propertyContainer, Action<T> configureDelegate)
        where T : notnull, new()
    {
        ArgumentNullException.ThrowIfNull(propertyContainer);

        propertyContainer.Properties.Configure(configureDelegate);
    }
}
