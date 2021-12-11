namespace Kantaiko.Properties.Immutable;

public interface IImmutablePropertyCollection : IReadOnlyPropertyCollection
{
    IImmutablePropertyCollection Set<T>(T value) where T : notnull;
    IImmutablePropertyCollection Remove(Type propertiesType);
}
