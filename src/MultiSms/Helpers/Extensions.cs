using MultiSms.Models;

namespace MultiSms.Helpers;

public static class Extensions
{
    public static bool IsValid(this string value)
    {
        return !(string.IsNullOrEmpty(value) || string.IsNullOrWhiteSpace(value));
    }

    public static ProviderData GetData(this IEnumerable<ProviderData> data, string key)
    {
        return data.FirstOrDefault(e => e.Key == key);
    }

}

