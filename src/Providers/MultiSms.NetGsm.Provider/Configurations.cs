using MultiSms.Interfaces;
using MultiSms.NetGsm.Provider;
using MultiSms.NetGsm.Provider.Options;

namespace Microsoft.Extensions.DependencyInjection;

public static class Configurations {
  public static MultiSmsBuilder UseNetGsm(this MultiSmsBuilder builder,
                                          string username, string password) {
    return builder.UseNetGsm(username, password, null);
  }

  public static MultiSmsBuilder UseNetGsm(this MultiSmsBuilder builder,
                                          string username, string password,
                                          string orginator) {
    return builder.UseNetGsm(options => {
      options.Username = username;
      options.Password = password;
      options.Orginator = orginator;
    });
  }

  public static MultiSmsBuilder
  UseNetGsm(this MultiSmsBuilder builder,
            Action<NetGsmProviderOptions> config) {
    var configuration = new NetGsmProviderOptions();
    config(configuration);

    configuration.Validate();

    builder.ServiceCollection.AddSingleton((s) => configuration);
    builder.ServiceCollection.AddHttpClient<ISmsProvider, NetGsmProvider>();
    builder.ServiceCollection.AddHttpClient<INetGsmProvider, NetGsmProvider>();

    return builder;
  }
}
