namespace MultiSms.Helpers;

public static class Extensions
{
    public static bool IsValid(this string value)
    {
        return !(string.IsNullOrEmpty(value) || string.IsNullOrWhiteSpace(value));
    }
}

