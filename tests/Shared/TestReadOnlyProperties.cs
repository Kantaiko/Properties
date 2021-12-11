namespace Kantaiko.Properties.Tests.Shared;

internal record TestReadOnlyProperties : ReadOnlyPropertiesBase<TestReadOnlyProperties>
{
    public int A { get; init; }
}
