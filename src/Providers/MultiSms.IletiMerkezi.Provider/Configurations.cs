using MultiSms.IletiMerkezi.Provider;
using MultiSms.IletiMerkezi.Provider.Options;
using MultiSms.Interfaces;

namespace Microsoft.Extensions.DependencyInjection;

public static class Configurations {
  public static MultiSmsBuilder UseIletiMerkezi(this MultiSmsBuilder builder,
                                                string username,
                                                string password) {
    return builder.UseIletiMerkezi(username, password, null);
  }

  public static MultiSmsBuilder UseIletiMerkezi(this MultiSmsBuilder builder,
                                                string username,
                                                string password,
                                                string orginator) {
    return builder.UseIletiMerkezi(options => {
      options.Username = username;
      options.Password = password;
      options.Orginator = orginator;
    });
  }

  public static MultiSmsBuilder
  UseIletiMerkezi(this MultiSmsBuilder builder,
                  Action<IletiMerkeziProviderOptions> config) {
    var configuration = new IletiMerkeziProviderOptions();
    config(configuration);

    configuration.Validate();

    builder.ServiceCollection.AddSingleton((s) => configuration);
    builder.ServiceCollection
        .AddHttpClient<ISmsProvider, IletiMerkeziProvider>();
    builder.ServiceCollection
        .AddHttpClient<IIletiMerkeziProvider, IletiMerkeziProvider>();

    return builder;
  }
}
