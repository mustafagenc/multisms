using MultiSms.Interfaces;
using MultiSms.Twilio.Provider;
using MultiSms.Twilio.Provider.Options;

namespace Microsoft.Extensions.DependencyInjection;

public static class Configurations {
  public static MultiSmsBuilder UseTwilio(this MultiSmsBuilder builder,
                                          string username, string password) {
    return builder.UseTwilio(username, password, null);
  }

  public static MultiSmsBuilder UseTwilio(this MultiSmsBuilder builder,
                                          string username, string password,
                                          string accountSID) {
    return builder.UseTwilio(options => {
      options.Username = username;
      options.Password = password;
      options.AccountSID = accountSID;
    });
  }

  public static MultiSmsBuilder
  UseTwilio(this MultiSmsBuilder builder,
            Action<TwilioProviderOptions> config) {
    var configuration = new TwilioProviderOptions();
    config(configuration);

    configuration.Validate();

    builder.ServiceCollection.AddSingleton((s) => configuration);
    builder.ServiceCollection.AddScoped<ISmsProvider, TwilioProvider>();
    builder.ServiceCollection.AddScoped<ITwilioProvider, TwilioProvider>();

    return builder;
  }
}