using MultiSms.Exceptions;

namespace MultiSms.SmsVitrini.Provider.Options;

public class SmsVitriniProviderOptions
{
    public string Username { get; set; }
    public string Password { get; set; }
    public string Orginator { get; set; }
    public string BaseUrl { get; set; } = "http://api.smsvitrini.com/index.php";

    public void Validate()
    {
        if (string.IsNullOrWhiteSpace(Username))
            throw new RequiredOptionException<SmsVitriniProviderOptions>($"{nameof(Username)}", "Kullanici adini girmelisiniz.");

        if (string.IsNullOrWhiteSpace(Password))
            throw new RequiredOptionException<SmsVitriniProviderOptions>($"{nameof(Password)}", "Sifrenizi girmelisiniz.");

        if (string.IsNullOrWhiteSpace(Orginator))
            throw new RequiredOptionException<SmsVitriniProviderOptions>($"{nameof(Orginator)}", "Orginator girmelisiniz.");
    }
}