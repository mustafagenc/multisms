using MultiSms.Exceptions;

namespace MultiSms.JetSms.Provider.Options;

public class JetSmsProviderOptions
{
    public string Username { get; set; }
    public string Password { get; set; }
    public string Orginator { get; set; }
    public string BaseUrl { get; set; } = "https://ws.jetsms.com.tr/api/";

    public void Validate()
    {
        if (string.IsNullOrWhiteSpace(Username))
            throw new RequiredOptionException<JetSmsProviderOptions>($"{nameof(Username)}", "Kullanici adini girmelisiniz.");

        if (string.IsNullOrWhiteSpace(Password))
            throw new RequiredOptionException<JetSmsProviderOptions>($"{nameof(Password)}", "Sifrenizi girmelisiniz.");

        if (string.IsNullOrWhiteSpace(Orginator))
            throw new RequiredOptionException<JetSmsProviderOptions>($"{nameof(Orginator)}", "Orginator girmelisiniz.");
    }
}