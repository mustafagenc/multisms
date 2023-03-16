using MultiSms.Exceptions;

namespace MultiSms.Twilio.Provider.Options;

public class TwilioProviderOptions
{
    public string Username { get; set; }

    public string Password { get; set; }

    public string AccountSID { get; set; }

    public void Validate()
    {
        if (string.IsNullOrWhiteSpace(Username))
            throw new RequiredOptionException<TwilioProviderOptions>($"{nameof(Username)}", "Kullanici adini girmelisiniz.");

        if (string.IsNullOrWhiteSpace(Password))
            throw new RequiredOptionException<TwilioProviderOptions>($"{nameof(Password)}", "Sifrenizi girmelisiniz.");
    }
}

