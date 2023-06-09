using MultiSms.Interfaces;
using MultiSms.Models;

namespace MultiSms.Factories;

public class MultiSmsServiceFactory {
  private readonly MultiSmsServiceOptions _options = new();
  private readonly HashSet<ISmsProvider> _providers = new();

  private MultiSmsServiceFactory() {}

  public static readonly MultiSmsServiceFactory Instance = new();

  public MultiSmsServiceFactory
  UseOptions(Action<MultiSmsServiceOptions> options) {
    if (options is null) {
      throw new ArgumentNullException(nameof(options));
    }

    options(_options);
    _options.Validate();

    return this;
  }

  public MultiSmsServiceFactory UseProvider(ISmsProvider provider) {
    if (provider is null) {
      throw new ArgumentNullException(nameof(provider));
    }

    _providers.Add(provider);

    return this;
  }

  public ISmsService Create() => new SmsService(_providers, _options);
}
