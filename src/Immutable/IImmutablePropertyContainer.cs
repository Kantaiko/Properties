namespace Kantaiko.Properties.Immutable;

public interface IImmutablePropertyContainer : IReadOnlyPropertyContainer
{
    new IImmutablePropertyCollection Properties { get; }

    IReadOnlyPropertyCollection IReadOnlyPropertyContainer.Properties => Properties;
}
