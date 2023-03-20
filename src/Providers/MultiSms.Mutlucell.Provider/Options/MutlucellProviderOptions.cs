using MultiSms.Exceptions;

namespace MultiSms.Mutlucell.Provider.Options;

public class MutlucellProviderOptions
{
    public string Username { get; set; }
    public string Password { get; set; }
    public string Orginator { get; set; }
    public string BaseUrl { get; set; } = "https://smsgw.mutlucell.com/";

    public void Validate()
    {
        if (string.IsNullOrWhiteSpace(Username))
            throw new RequiredOptionException<MutlucellProviderOptions>($"{nameof(Username)}", "Kullanici adini girmelisiniz.");

        if (string.IsNullOrWhiteSpace(Password))
            throw new RequiredOptionException<MutlucellProviderOptions>($"{nameof(Password)}", "Sifrenizi girmelisiniz.");

        if (string.IsNullOrWhiteSpace(Orginator))
            throw new RequiredOptionException<MutlucellProviderOptions>($"{nameof(Orginator)}", "Orginator girmelisiniz.");
    }
}