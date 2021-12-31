namespace Kantaiko.Properties.Immutable;

public static class ImmutablePropertyCollectionExtensions
{
    public static IImmutablePropertyCollection Remove<T>(this IImmutablePropertyCollection propertyCollection)
        where T : notnull
    {
        ArgumentNullException.ThrowIfNull(propertyCollection);

        return propertyCollection.Remove(typeof(T));
    }

    public static IImmutablePropertyCollection Update<T>(this IImmutablePropertyCollection propertyCollection,
        Func<T, T> updateDelegate)
        where T : notnull, new()
    {
        ArgumentNullException.ThrowIfNull(updateDelegate);

        var properties = GetOrCreate<T>(propertyCollection);

        return propertyCollection.Set(updateDelegate(properties));
    }

    private static T GetOrCreate<T>(IImmutablePropertyCollection propertyCollection) where T : notnull, new()
    {
        ArgumentNullException.ThrowIfNull(propertyCollection);

        return propertyCollection.TryGet<T>(out var properties) ? properties : new T();
    }
}
