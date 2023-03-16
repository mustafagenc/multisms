using MultiSms.Exceptions;
using MultiSms.Interfaces;
using MultiSms.Models;

namespace MultiSms;

public partial class SmsService : ISmsService
{
    private readonly IDictionary<string, ISmsProvider> _providers;
    private readonly ISmsProvider _defaultProvider;

    public MultiSmsServiceOptions Options { get; }
    public IEnumerable<ISmsProvider> Providers => _providers.Values;
    public ISmsProvider DefaultProvider => _defaultProvider;

    public SendingResult Send(MessageBody message)
    {
        throw new NotImplementedException();
    }

    public SendingResult Send(MessageBody message, string providerName)
    {
        throw new NotImplementedException();
    }

    public SendingResult Send(MessageBody message, ISmsProvider provider)
    {
        throw new NotImplementedException();
    }

    public Task<SendingResult> SendAsync(MessageBody message, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<SendingResult> SendAsync(MessageBody message, string providerName, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<SendingResult> SendAsync(MessageBody message, ISmsProvider provider, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public SmsService(IEnumerable<ISmsProvider> providers, MultiSmsServiceOptions options)
    {
        if (providers is null)
            throw new ArgumentNullException(nameof(providers));

        if (!providers.Any())
            throw new ArgumentException("En az bir adet saglayici girmelisiniz.", nameof(providers));

        if (options is null)
            throw new ArgumentNullException(nameof(options));

        options.Validate();

        Options = options;

        _providers = providers.ToDictionary(provider => provider.Name);

        if (!_providers.ContainsKey(options.DefaultProvider))
            throw new ProviderNotFoundException(options.DefaultProvider);

        _defaultProvider = _providers[options.DefaultProvider];
    }

    private void CheckMessageOrginatorValue(MessageBody message)
    {
        if (message.Originator is null)
        {
            if (Options.DefaultOrginator is null)
                throw new ArgumentException($"{typeof(MessageBody).FullName} orginator bilgisi yok.");

            message.SetFrom(Options.DefaultOrginator);
        }
    }

}