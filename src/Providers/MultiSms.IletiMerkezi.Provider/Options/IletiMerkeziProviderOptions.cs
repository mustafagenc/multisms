using MultiSms.Exceptions;

namespace MultiSms.IletiMerkezi.Provider.Options;

public class IletiMerkeziProviderOptions {
  public string Username { get; set; }
  public string Password { get; set; }
  public string Orginator { get; set; }
  public string BaseUrl { get; set; } = "https://api.iletimerkezi.com/";

  public void Validate() {
    if (string.IsNullOrWhiteSpace(Username))
      throw new RequiredOptionException<IletiMerkeziProviderOptions>(
          $"{nameof(Username)}", "Kullanici adini girmelisiniz.");

    if (string.IsNullOrWhiteSpace(Password))
      throw new RequiredOptionException<IletiMerkeziProviderOptions>(
          $"{nameof(Password)}", "Sifrenizi girmelisiniz.");

    if (string.IsNullOrWhiteSpace(Orginator))
      throw new RequiredOptionException<IletiMerkeziProviderOptions>(
          $"{nameof(Orginator)}", "Orginator girmelisiniz.");
  }
}