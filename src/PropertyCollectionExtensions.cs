namespace Kantaiko.Properties;

public static class PropertyCollectionExtensions
{
    public static void Configure<T>(this IPropertyCollection propertyCollection, Action<T> configureDelegate)
        where T : notnull, new()
    {
        ArgumentNullException.ThrowIfNull(propertyCollection);
        ArgumentNullException.ThrowIfNull(configureDelegate);

        configureDelegate.Invoke(propertyCollection.GetOrCreate<T>());
    }

    public static void Update<T>(this IPropertyCollection propertyCollection, Func<T, T> updateDelegate)
        where T : notnull, new()
    {
        ArgumentNullException.ThrowIfNull(propertyCollection);
        ArgumentNullException.ThrowIfNull(updateDelegate);

        var value = updateDelegate.Invoke(propertyCollection.GetOrCreate<T>());
        propertyCollection.Set(value);
    }

    public static bool Remove<T>(this IPropertyCollection propertyCollection) where T : notnull
    {
        ArgumentNullException.ThrowIfNull(propertyCollection);

        return propertyCollection.Remove(typeof(T));
    }
}
