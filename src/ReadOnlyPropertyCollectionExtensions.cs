using Kantaiko.Properties.Immutable;

namespace Kantaiko.Properties;

public static class ReadOnlyPropertyCollectionExtensions
{
    public static T? Get<T>(this IReadOnlyPropertyCollection propertyCollection) where T : notnull
    {
        return propertyCollection.TryGet<T>(out var properties) ? properties : default;
    }

    public static IImmutablePropertyCollection ToImmutable(this IReadOnlyPropertyCollection propertyCollection)
    {
        if (propertyCollection is IImmutablePropertyCollection immutablePropertyCollection)
        {
            return immutablePropertyCollection;
        }

        return new ImmutablePropertyCollection(propertyCollection.Objects);
    }
}
