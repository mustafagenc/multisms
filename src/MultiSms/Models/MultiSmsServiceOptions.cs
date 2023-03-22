using MultiSms.Exceptions;
using MultiSms.Helpers;

namespace MultiSms.Models;

public class MultiSmsServiceOptions {
  public string DefaultProvider { get; set; }

  public string DefaultOrginator { get; set; }

  public void Validate() {
    if (!DefaultProvider.IsValid()) {
      throw new RequiredOptionException<MultiSmsServiceOptions>(
          nameof(DefaultProvider), "Varsayilan saglayiciyi secin.");
    }
  }
}
