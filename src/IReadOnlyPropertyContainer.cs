namespace Kantaiko.Properties;

/// <summary>
/// Represents an object that contains readonly user-defined properties.
/// </summary>
public interface IReadOnlyPropertyContainer
{
    IReadOnlyPropertyCollection Properties { get; }
}
