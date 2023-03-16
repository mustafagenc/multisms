using System;
using System.Threading.Channels;
using MultiSms.Models;
using MultiSms.Interfaces;

namespace MultiSms.Factories;

public partial class MultiSmsServiceFactory
{
    private readonly MultiSmsServiceOptions _options = new MultiSmsServiceOptions();
    private readonly HashSet<ISmsProvider> _providers = new HashSet<ISmsProvider>();

    private MultiSmsServiceFactory() { }

    public static readonly MultiSmsServiceFactory Instance = new MultiSmsServiceFactory();

    public MultiSmsServiceFactory UseOptions(Action<MultiSmsServiceOptions> options)
    {
        if (options is null)
            throw new ArgumentNullException(nameof(options));

        options(_options);
        _options.Validate();

        return this;
    }

    public MultiSmsServiceFactory UseProvider(ISmsProvider provider)
    {
        if (provider is null)
            throw new ArgumentNullException(nameof(provider));

        _providers.Add(provider);

        return this;
    }

    public ISmsService Create() => new SmsService(_providers, _options);
}