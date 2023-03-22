using MultiSms.IletiMerkezi.Provider;
using MultiSms.IletiMerkezi.Provider.Options;
using MultiSms.Interfaces;

namespace Microsoft.Extensions.DependencyInjection;

public static class Configurations {
  public static MultiSmsBuilder UseIletiMerkezi(this MultiSmsBuilder builder,
                                                string key, string hash) {
    return builder.UseIletiMerkezi(key, hash, null);
  }

  public static MultiSmsBuilder UseIletiMerkezi(this MultiSmsBuilder builder,
                                                string key, string hash,
                                                string orginator) {
    return builder.UseIletiMerkezi(options => {
      options.Key = key;
      options.Hash = hash;
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
