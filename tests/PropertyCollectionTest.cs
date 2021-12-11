using Kantaiko.Properties.Tests.Shared;
using Xunit;

namespace Kantaiko.Properties.Tests;

public class PropertyCollectionTest
{
    [Fact]
    public void ShouldConfigureAndReadProperties()
    {
        var propertyCollection = new PropertyCollection();

        propertyCollection.Configure<TestProperties>(properties => properties.A = 42);

        var properties = propertyCollection.Get<TestProperties>();
        Assert.Equal(42, properties?.A);
    }

    [Fact]
    public void ShouldUpdateAndReadReadOnlyProperties()
    {
        var propertyCollection = new PropertyCollection();

        propertyCollection.Update<TestReadOnlyProperties>(properties => properties with { A = 42 });

        var properties = propertyCollection.Get<TestReadOnlyProperties>();
        Assert.Equal(42, properties?.A);
    }

    [Fact]
    public void ShouldSetAndRemoveProperties()
    {
        var propertyCollection = new PropertyCollection();

        propertyCollection.Set(new TestProperties());
        propertyCollection.Remove<TestProperties>();

        Assert.Empty(propertyCollection.Objects);
    }

    [Fact]
    public void ShouldClearCollection()
    {
        var propertyCollection = new PropertyCollection();

        propertyCollection.Set(new TestProperties());
        propertyCollection.Clear();

        Assert.Empty(propertyCollection.Objects);
    }

    [Fact]
    public void ShouldReturnDefaultIfPropertiesNotFound()
    {
        var propertyCollection = new PropertyCollection();

        var properties = propertyCollection.Get<TestProperties>();
        Assert.Equal(default, properties);
    }

    [Fact]
    public void ShouldCreatePropertyCollectionFromObjects()
    {
        var propertyCollection = new PropertyCollection(new[] { new TestProperties() });

        var properties = propertyCollection.Get<TestProperties>();
        Assert.NotNull(properties);
    }
}
