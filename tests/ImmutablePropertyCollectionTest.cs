using Kantaiko.Properties.Immutable;
using Kantaiko.Properties.Tests.Shared;
using Xunit;

namespace Kantaiko.Properties.Tests;

public class ImmutablePropertyCollectionTest
{
    [Fact]
    public void ShouldAppendPropertiesToImmutableCollection()
    {
        IImmutablePropertyCollection propertyCollection = new ImmutablePropertyCollection();

        propertyCollection = propertyCollection.Set(new TestReadOnlyProperties { A = 42 });

        var properties = propertyCollection.Get<TestReadOnlyProperties>();
        Assert.Equal(42, properties?.A);
    }

    [Fact]
    public void ShouldRemovePropertiesFromImmutableCollection()
    {
        IImmutablePropertyCollection propertyCollection = new ImmutablePropertyCollection();

        propertyCollection = propertyCollection.Set(new TestReadOnlyProperties { A = 42 });
        propertyCollection = propertyCollection.Remove<TestReadOnlyProperties>();

        Assert.Empty(propertyCollection.Objects);
    }

    [Fact]
    public void ShouldCreateImmutableCollectionFromPropertyCollection()
    {
        var propertyCollection = new PropertyCollection();
        propertyCollection.Set(new TestProperties());

        var immutablePropertyCollection = propertyCollection.ToImmutable();

        var properties = immutablePropertyCollection.Get<TestProperties>();
        Assert.NotNull(properties);
    }

    [Fact]
    public void ShouldReturnSameInstanceIfPropertyCollectionIsAlreadyImmutable()
    {
        IReadOnlyPropertyCollection propertyCollection = ImmutablePropertyCollection.Empty;
        var immutablePropertyCollection = propertyCollection.ToImmutable();

        Assert.Same(propertyCollection, immutablePropertyCollection);
    }

    [Fact]
    public void ShouldReturnSameCollectionIfNotChangedAfterSet()
    {
        var properties = new TestProperties();

        var firstCollection = new ImmutablePropertyCollection(new[] { properties });
        var secondCollection = firstCollection.Set(properties);

        Assert.Equal(firstCollection, secondCollection);
    }

    [Fact]
    public void ShouldReturnSameCollectionIfNotChangedAfterRemove()
    {
        var firstCollection = new ImmutablePropertyCollection();
        var secondCollection = firstCollection.Remove<TestProperties>();

        Assert.Equal(firstCollection, secondCollection);
    }

    [Fact]
    public void ShouldReturnDefaultIfPropertiesNotFound()
    {
        var propertyCollection = new ImmutablePropertyCollection();

        var properties = propertyCollection.Get<TestProperties>();
        Assert.Equal(default, properties);
    }

    [Fact]
    public void ShouldUpdatePropertiesInImmutablePropertyCollection()
    {
        IImmutablePropertyCollection propertyCollection = new ImmutablePropertyCollection();

        propertyCollection = propertyCollection.Update<TestReadOnlyProperties>(props => props with
        {
            A = props.A + 42
        });

        var properties = propertyCollection.Get<TestReadOnlyProperties>();
        Assert.Equal(42, properties?.A);
    }
}
