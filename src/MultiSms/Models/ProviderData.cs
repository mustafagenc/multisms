using MultiSms.Helpers;

namespace MultiSms.Models;

public readonly struct ProviderData : IEquatable<ProviderData>
{
    public string Key { get; }

    public object Value { get; }

    public ProviderData(string key, object value)
    {
        Key = key ?? throw new ArgumentNullException(nameof(key));
        Value = value ?? throw new ArgumentNullException(nameof(value));

        if (!Key.IsValid())
        {
            throw new ArgumentException("Key degeri bos.");
        }
    }

    public static ProviderData New(string key, object value) => new(key, value);

    public TValue GetValue<TValue>() => (TValue)Value;

    public bool IsEmpty() => !Key.IsValid() && Value == default;

    public override string ToString() => $"key: {Key} | value: {Value}";

    public override bool Equals(object obj)
    {
        if (obj is null)
        {
            return false;
        }

        if (obj is ProviderData data)
        {
            return Equals(data);
        }

        return false;
    }

    public bool Equals(ProviderData other) => other.Key == Key;

    public override int GetHashCode()
    {
        return HashCode.Combine(Key);
    }

    public static bool operator ==(ProviderData left, ProviderData right) => EqualityComparer<ProviderData>.Default.Equals(left, right);

    public static bool operator !=(ProviderData left, ProviderData right) => !(left == right);
}