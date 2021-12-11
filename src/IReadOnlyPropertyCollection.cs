using System.Diagnostics.CodeAnalysis;

namespace Kantaiko.Properties;

public interface IReadOnlyPropertyCollection
{
    bool TryGet<T>([NotNullWhen(true)] out T? properties) where T : notnull;

    IEnumerable<object> Objects { get; }
}
