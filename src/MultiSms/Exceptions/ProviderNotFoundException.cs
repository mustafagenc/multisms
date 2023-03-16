namespace MultiSms.Exceptions;

[Serializable]
public class ProviderNotFoundException : Exception
{
    private static readonly string message = "{{name}} adinda bir saglayici bulunamadi.";

    public string ProviderName { get; set; }

    public ProviderNotFoundException(string providerName)
        : base(message.Replace("{{name}}", providerName)) { }

    public ProviderNotFoundException(string message, string providerName)
        : base(message)
    {
        ProviderName = providerName;
    }

    protected ProviderNotFoundException(
      System.Runtime.Serialization.SerializationInfo info,
      System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
}

