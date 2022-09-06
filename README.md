# Kantaiko.Properties

[![Build Status](https://img.shields.io/github/workflow/status/Kantaiko/Properties/Test?style=flat-square)](https://github.com/Kantaiko/Properties/actions/workflows/test.yml)
[![Coverage](https://img.shields.io/codecov/c/github/Kantaiko/Properties?style=flat-square)](https://codecov.io/gh/Kantaiko/Properties)
[![NuGet](https://img.shields.io/nuget/v/Kantaiko.Properties.svg?style=flat-square)](https://www.nuget.org/packages/Kantaiko.Properties/)
[![NuGet](https://img.shields.io/nuget/dt/Kantaiko.Properties.svg?style=flat-square)](https://www.nuget.org/packages/Kantaiko.Properties/)

A simple and predictable property-based extension system.

## Usage

The main goal of this library is to provide a convenient way to extend objects with user defined properties.

In fact, this can be achieved by using a simple `Dictionary<string, object>` property,
but this approach is not very convenient, especially when you build a public API.

### Property collection

This library provides a special collection type, `PropertyCollection`, which can be used to store and retrieve
properties:

```csharp
internal class MyProperties
{
    public int Count { get; set; }
}

var collection = new PropertyCollection();

collection.Configure<MyProperties>(properties => properties.Count = 5);

var count = collection.Get<MyProperties>()?.Count ?? 0;
```

As you can see, the properties are stored in a separate class,
which is also used to identify the properties in the collection.

There is also a `Configure` method which allows to set object properties in a single call.
If you are familiar with the `Microsoft.Extensions.Options` library, this method is similar to the `Configure<TOptions>`
method.

The property collection can store any object, but it is recommended to use immutable objects were possible:

```csharp
internal record MyProperties
{
    public int Count { get; }
}

var collection = new PropertyCollection();

collection.Update<MyProperties>(properties => properties with { Count = 5 });

var count = collection.Get<MyProperties>()?.Count ?? 0;
```

There is an another method, `Update`, which allows to update the properties in a single call.
If the collection does not contain an object of the specified type,
it will be created and passed to the update function.

If you want to update your immutable properties, an empty parameterless constructor is required.
Otherwise, you can use the `Set` method to set the properties directly:

```csharp
internal record MyProperties(int Count);

var collection = new PropertyCollection();

collection.Set(new MyProperties(5));

var count = collection.Get<MyProperties>()?.Count ?? 0;
```

### Property container

The `PropertyCollection` class is not very convenient to use in real-world scenarios,
but is required to encapsulate the properties in a separate class.
In most cases, we want to extend an existing class with properties,
so this class can implement the `IPropertyContainer` interface:

```csharp
internal class MyProperties
{
    public int Count { get; set; }
}

internal class MyObject : IPropertyContainer
{
    public int SomeKnownProperty { get; set; }

    public IPropertyCollection Properties { get; } = new PropertyCollection();
}

var obj = new MyObject();

obj.Configure<MyProperties>(properties => properties.Count = 5);

var count = obj.Properties.Get<MyProperties>()?.Count ?? 0;
```

As you can see, the `IPropertyContainer` interface provides the same methods as the `PropertyCollection` class,
but we don't need to mess known object properties with the `PropertyCollection`.
However, this approach still requires us to access `Properties` property in order to retrieve the properties.

To make this process more convenient, the property class can extend the `PropertyClass` class:

```csharp
internal class MyProperties : PropertyClass
{
    public int Count { get; set; }
}

internal class MyObject : PropertyContainer
{
    public IPropertyCollection Properties { get; } = new PropertyCollection();
}

var obj = new MyObject();

obj.Configure<MyProperties>(properties => properties.Count = 5);

if (MyProperties.Of(obj) is { Count: var count })
{
    // do something with the count
}
```

The `PropertyClass` class provides the static `Of` method which is "inherited" by the property class.
As you can see, this feature is very convenient to use with C# 9 pattern matching.

There is also a `PropertyRecord` class which can be used to inherit from a record class:

```csharp
internal record MyProperties(int Count) : PropertyRecord;

internal class MyObject : PropertyContainer
{
    public IPropertyCollection Properties { get; } = new PropertyCollection();
}

var obj = new MyObject();

obj.Set(new MyProperties(5));

if (MyProperties.Of(obj) is { Count: var count })
{
    // do something with the count
}
```

### Immutable property collection

There is also an immutable version of the property collection, `ImmutablePropertyCollection`.

This class may be useful if you want to create a copy of an object with some properties changed:

```csharp
internal record MyProperties(int Count) : PropertyRecord;

internal record MyObject(
    int SomeKnownProperty,
    IImmutablePropertyCollection Properties = ImmutablePropertyCollection.Empty
) : ImmutablePropertyContainer;

var obj = new MyObject(42);

obj = obj with { Properties = obj.Properties.Set(new MyProperties(5)) };

if (MyProperties.Of(obj) is { Count: var count })
{
    // do something with the count
}
```

As you can see, there is a `Set` method which returns a new collection
with the specified properties added or replaced.
There is also a static `Empty` property which can be used to avoid creating a new empty collection every time.

### Read-only property collection

Both `PropertyCollection` and `ImmutablePropertyCollection` implement the `IReadOnlyPropertyCollection` interface,
which can used only for reading properties:

```csharp
internal record MyProperties(int Count) : PropertyRecord;

internal class MyObject : ReadOnlyPropertyContainer
{
    public MyObject(IReadOnlyPropertyCollection properties)
    {
        Properties = properties;
    }

    public IReadOnlyPropertyCollection Properties { get; }
}

var obj = new MyObject(ImmutablePropertyCollection.Empty.Set(new MyProperties(5)));

if (MyProperties.Of(obj) is { Count: var count })
{
    // do something with the count
}
```

## Contributing

Pull requests are welcome. For major changes, please open an issue first to discuss what you would like to change.

Please make sure to update tests as appropriate.

## License

[MIT](https://choosealicense.com/licenses/mit/)
