namespace Kantaiko.Properties.Immutable;

public static class ImmutablePropertiesExtensions
{
    public static IImmutablePropertyCollection Remove<T>(this IImmutablePropertyCollection propertyCollection)
        where T : notnull
    {
        ArgumentNullException.ThrowIfNull(propertyCollection);

        return propertyCollection.Remove(typeof(T));
    }
}
