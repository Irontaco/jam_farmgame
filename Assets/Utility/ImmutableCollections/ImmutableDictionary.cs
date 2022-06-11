using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

public readonly struct ImmutableDictionary<TKey, TValue> : IReadOnlyCollection<KeyValuePair<TKey, TValue>>
{
    private readonly Dictionary<TKey, TValue> _underlyingDictionary;

    public ImmutableDictionary(IEnumerable<KeyValuePair<TKey, TValue>> enumerable)
    {
        _underlyingDictionary = enumerable.ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
    }
    
    public ImmutableDictionary(IEnumerable<(TKey Key, TValue Value)> enumerable)
        : this(enumerable.Select(kvp => new KeyValuePair<TKey, TValue>(kvp.Key, kvp.Value))) { }

    public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
    {
        return _underlyingDictionary?.GetEnumerator()
               ?? Enumerable.Empty<KeyValuePair<TKey, TValue>>().GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public bool Contains(KeyValuePair<TKey, TValue> item)
    {
        return _underlyingDictionary?.Contains(item) ?? false;
    }

    public int Count => _underlyingDictionary?.Count ?? 0;
    public bool IsReadOnly => true;

    public bool ContainsKey(TKey key)
    {
        return _underlyingDictionary?.ContainsKey(key) ?? false;
    }

    public bool TryGetValue(TKey key, out TValue value)
    {
        if (_underlyingDictionary is null)
        {
            value = default;
            return false;
        }
        return _underlyingDictionary.TryGetValue(key, out value);
    }

    public TValue this[TKey key]
    {
        get => _underlyingDictionary is null ? throw new ArgumentOutOfRangeException() : _underlyingDictionary[key];
    }

    public IReadOnlyCollection<TKey> Keys => _underlyingDictionary?.Keys ?? (IReadOnlyCollection<TKey>)Array.Empty<TKey>();
    public IReadOnlyCollection<TValue> Values => _underlyingDictionary?.Values ?? (IReadOnlyCollection<TValue>)Array.Empty<TValue>();
}

public static class ImmutableDictionary
{
    public static ImmutableDictionary<TKey, TValue> ToImmutableDictionary<TKey, TValue>(
        this IEnumerable<KeyValuePair<TKey, TValue>> enumerable)
    {
        return new ImmutableDictionary<TKey, TValue>(enumerable);
    }
    
    public static ImmutableDictionary<TKey, TValue> ToImmutableDictionary<TKey, TValue>(
        this IEnumerable<(TKey Key, TValue Value)> enumerable)
    {
        return new ImmutableDictionary<TKey, TValue>(enumerable);
    }
}