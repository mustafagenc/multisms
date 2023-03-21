using MultiSms.Exceptions;
using MultiSms.Interfaces;
using MultiSms.Models;

namespace MultiSms;

public class SmsService : ISmsService
{
    public MultiSmsServiceOptions Options { get; }
    public IEnumerable<ISmsProvider> Providers => _providers.Values;
    public ISmsProvider DefaultProvider => _defaultProvider;

    public SendingResult Send(MessageBody message)
    {
        return Send(message, _defaultProvider);
    }

    public SendingResult Send(MessageBody message, string providerName)
    {
        if (providerName is null)
        {
            throw new ArgumentNullException(nameof(providerName));
        }

        if (!_providers.TryGetValue(providerName, out ISmsProvider provider))
        {
            throw new ProviderNotFoundException(providerName);
        }

        return Send(message, provider);
    }

    public SendingResult Send(MessageBody message, ISmsProvider provider)
    {
        if (message is null)
        {
            throw new ArgumentNullException(nameof(message));
        }

        if (provider is null)
        {
            throw new ArgumentNullException(nameof(provider));
        }

        CheckMessageOrginatorValue(message);

        return provider.Send(message);
    }

    public Task<SendingResult> SendAsync(MessageBody message, CancellationToken cancellationToken = default)
    {
        return SendAsync(message, _defaultProvider, cancellationToken);
    }

    public Task<SendingResult> SendAsync(MessageBody message, string providerName, CancellationToken cancellationToken = default)
    {
        if (providerName is null)
        {
            throw new ArgumentNullException(nameof(providerName));
        }

        if (!_providers.TryGetValue(providerName, out ISmsProvider provider))
        {
            throw new ProviderNotFoundException(providerName);
        }

        return SendAsync(message, provider, cancellationToken);
    }

    public Task<SendingResult> SendAsync(MessageBody message, ISmsProvider provider, CancellationToken cancellationToken = default)
    {
        if (message is null)
        {
            throw new ArgumentNullException(nameof(message));
        }

        if (provider is null)
        {
            throw new ArgumentNullException(nameof(provider));
        }

        CheckMessageOrginatorValue(message);

        return provider.SendAsync(message, cancellationToken);
    }

    public SmsService(IEnumerable<ISmsProvider> providers, MultiSmsServiceOptions options)
    {
        if (providers is null)
        {
            throw new ArgumentNullException(nameof(providers));
        }

        if (!providers.Any())
        {
            throw new ArgumentException("En az bir adet saglayici girmelisiniz.", nameof(providers));
        }

        if (options is null)
        {
            throw new ArgumentNullException(nameof(options));
        }

        options.Validate();
        Options = options;
        _providers = providers.ToDictionary(provider => provider.Name);

        if (!_providers.ContainsKey(options.DefaultProvider))
        {
            throw new ProviderNotFoundException(options.DefaultProvider);
        }

        _defaultProvider = _providers[options.DefaultProvider];
    }

    private readonly IDictionary<string, ISmsProvider> _providers;

    private readonly ISmsProvider _defaultProvider;

    private void CheckMessageOrginatorValue(MessageBody message)
    {
        if (message.Originator is null)
        {
            if (Options.DefaultOrginator is null)
            {
                throw new ArgumentException($"{typeof(MessageBody).FullName} orginator bilgisi yok.");
            }

            message.SetFrom(Options.DefaultOrginator);
        }
    }
}