namespace MultiSms.Models;

/// <summary>
/// 
/// </summary>
public sealed class PhoneNumber : IEquatable<PhoneNumber>, IEquatable<string>
{
    private readonly string _phoneNumber;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="phoneNumber"></param>
    /// <exception cref="ArgumentNullException"></exception>
    /// <exception cref="ArgumentException"></exception>
    public PhoneNumber(string phoneNumber)
    {
        if (phoneNumber is null)
            throw new ArgumentNullException(nameof(phoneNumber));

        if (string.IsNullOrEmpty(phoneNumber))
            throw new ArgumentException("Telefon numarasi bos olamaz.", nameof(phoneNumber));


        _phoneNumber = phoneNumber;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    public override bool Equals(object obj)
    {
        if (obj is null) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj is string stringValue) Equals(stringValue);
        if (obj is PhoneNumber number) Equals(number);

        return false;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="other"></param>
    /// <returns></returns>
    public bool Equals(PhoneNumber other)
    {
        return (other is not null && other.ToString().Equals(_phoneNumber, StringComparison.OrdinalIgnoreCase));
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="other"></param>
    /// <returns></returns>
    public bool Equals(string other)
    {
        return (other is not null && other.Equals(_phoneNumber, StringComparison.OrdinalIgnoreCase));
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(_phoneNumber);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public override string ToString() => _phoneNumber;

    public static implicit operator PhoneNumber(string phoneNumber) => new PhoneNumber(phoneNumber);

    public static implicit operator string(PhoneNumber phoneNumber) => phoneNumber.ToString();

    public static bool operator ==(PhoneNumber left, PhoneNumber right) => EqualityComparer<PhoneNumber>.Default.Equals(left, right);

    public static bool operator !=(PhoneNumber left, PhoneNumber right) => !(left == right);

}