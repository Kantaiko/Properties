namespace Kantaiko.Properties.Tests.Shared;

internal record TestReadOnlyProperties : PropertyRecord<TestReadOnlyProperties>
{
    public int A { get; init; }
}
