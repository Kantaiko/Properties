namespace Kantaiko.Properties.Immutable;

/// <summary>
/// Represents an object that contains immutable user-defined properties.
/// </summary>
public interface IImmutablePropertyContainer : IReadOnlyPropertyContainer
{
    new IImmutablePropertyCollection Properties { get; }

    IReadOnlyPropertyCollection IReadOnlyPropertyContainer.Properties => Properties;
}
