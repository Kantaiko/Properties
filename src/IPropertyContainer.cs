namespace Kantaiko.Properties;

/// <summary>
/// Represents an object that contains user-defined properties.
/// </summary>
public interface IPropertyContainer : IReadOnlyPropertyContainer
{
    new IPropertyCollection Properties { get; }

    IReadOnlyPropertyCollection IReadOnlyPropertyContainer.Properties => Properties;
}
