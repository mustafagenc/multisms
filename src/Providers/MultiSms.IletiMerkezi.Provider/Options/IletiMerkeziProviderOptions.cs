using MultiSms.Exceptions;

namespace MultiSms.IletiMerkezi.Provider.Options;

public class IletiMerkeziProviderOptions
{
    public string Key { get; set; }
    public string Hash { get; set; }
    public string Orginator { get; set; }
    public string BaseUrl { get; set; } = "https://api.iletimerkezi.com/";

    public void Validate()
    {
        if (string.IsNullOrWhiteSpace(Key))
        {
            throw new RequiredOptionException<IletiMerkeziProviderOptions>($"{nameof(Key)}", "API Anahtarıni girmelisiniz.");
        }

        if (string.IsNullOrWhiteSpace(Hash))
        {
            throw new RequiredOptionException<IletiMerkeziProviderOptions>($"{nameof(Hash)}", "Hash girmelisiniz.");
        }

        if (string.IsNullOrWhiteSpace(Orginator))
        {
            throw new RequiredOptionException<IletiMerkeziProviderOptions>($"{nameof(Orginator)}", "Orginator girmelisiniz.");
        }
    }
}