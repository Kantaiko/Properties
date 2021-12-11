namespace Kantaiko.Properties;

public interface IPropertyContainer : IReadOnlyPropertyContainer
{
    new IPropertyCollection Properties { get; }

    IReadOnlyPropertyCollection IReadOnlyPropertyContainer.Properties => Properties;
}
