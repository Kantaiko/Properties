using Kantaiko.Properties.Tests.Shared;
using Xunit;

namespace Kantaiko.Properties.Tests;

public class PropertyContainerTest
{
    private class MyObject : IPropertyContainer
    {
        public IPropertyCollection Properties { get; } = new PropertyCollection();
    }

    [Fact]
    public void ShouldConfigureAndReadProperties()
    {
        var myObject = new MyObject();

        myObject.Configure<TestProperties>(properties => properties.A = 42);

        Assert.Equal(42, TestProperties.Of(myObject)?.A);
    }

    [Fact]
    public void ShouldUpdateAndReadProperties()
    {
        var myObject = new MyObject();

        myObject.Update<TestReadOnlyProperties>(properties => properties with { A = 42 });

        Assert.Equal(42, TestReadOnlyProperties.Of(myObject)?.A);
    }
}
