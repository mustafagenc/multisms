using MultiSms.Exceptions;

namespace MultiSms.MasGsm.Provider.Options;

public class MasGsmProviderOptions
{
    public string Username { get; set; }
    public string Password { get; set; }
    public string Orginator { get; set; }
    public string BaseUrl { get; set; } = "https://api.senagsm.com.tr/api/";

    public void Validate()
    {
        if (string.IsNullOrWhiteSpace(Username))
            throw new RequiredOptionException<MasGsmProviderOptions>($"{nameof(Username)}", "Kullanici adini girmelisiniz.");

        if (string.IsNullOrWhiteSpace(Password))
            throw new RequiredOptionException<MasGsmProviderOptions>($"{nameof(Password)}", "Sifrenizi girmelisiniz.");

        if (string.IsNullOrWhiteSpace(Orginator))
            throw new RequiredOptionException<MasGsmProviderOptions>($"{nameof(Orginator)}", "Orginator girmelisiniz.");
    }
}