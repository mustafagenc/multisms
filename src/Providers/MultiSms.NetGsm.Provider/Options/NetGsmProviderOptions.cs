using MultiSms.Exceptions;

namespace MultiSms.NetGsm.Provider.Options;

public class NetGsmProviderOptions
{
    public string Username { get; set; }
    public string Password { get; set; }
    public string Orginator { get; set; }
    public string BaseUrl { get; set; } = "https://api.netgsm.com.tr/";

    public void Validate()
    {
        if (string.IsNullOrWhiteSpace(Username))
            throw new RequiredOptionException<NetGsmProviderOptions>($"{nameof(Username)}", "Kullanici adini girmelisiniz.");

        if (string.IsNullOrWhiteSpace(Password))
            throw new RequiredOptionException<NetGsmProviderOptions>($"{nameof(Password)}", "Sifrenizi girmelisiniz.");

        if (string.IsNullOrWhiteSpace(Orginator))
            throw new RequiredOptionException<NetGsmProviderOptions>($"{nameof(Orginator)}", "Orginator girmelisiniz.");
    }
}