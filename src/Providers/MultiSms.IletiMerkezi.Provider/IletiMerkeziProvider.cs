using System.Text;
using MultiSms.Helpers;
using MultiSms.IletiMerkezi.Provider.Models;
using MultiSms.IletiMerkezi.Provider.Options;
using MultiSms.Interfaces;
using MultiSms.Models;

namespace MultiSms.IletiMerkezi.Provider;

public partial class IletiMerkeziProvider : IIletiMerkeziProvider
{
    public SendingResult Send(MessageBody message)
    {
        return SendAsync(message).GetAwaiter().GetResult();
    }

    public async Task<SendingResult> SendAsync(MessageBody message, CancellationToken cancellationToken = default)
    {
        try
        {
            var client = CreateClient();

            using var request = new HttpRequestMessage(HttpMethod.Post, new UriBuilder(_options.BaseUrl) { Path = "sms/send/xml" }.Uri);
            using var xmlContent = new StringContent(CreateMessage(message).Serialize(), Encoding.UTF8, "application/xml");

            request.Content = xmlContent;

            using var response = await client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, cancellationToken).ConfigureAwait(false);

            return BuildResultObject(response);
        }
        catch (Exception ex)
        {
            return SendingResult.Failure(Name).AddError(ex);
        }
    }
}

public partial class IletiMerkeziProvider
{
    private readonly HttpClient _httpClient;
    private readonly IletiMerkeziProviderOptions _options;

    string ISmsProvider.Name => Name;

    public const string Name = "IletiMerkezi";

    public IletiMerkeziProvider(HttpClient httpClient, IletiMerkeziProviderOptions options)
    {
        if (options is null)
            throw new ArgumentNullException(nameof(options));

        options.Validate();
        _options = options;

        _httpClient = httpClient ?? new HttpClient();
    }

    private HttpClient CreateClient() => _httpClient;

    private static SendingResult BuildResultObject(HttpResponseMessage result)
    {
        return SendingResult.Success(Name).AddMetaData("response", result);
    }

    public IletiMerkeziMessage CreateMessage(MessageBody message)
    {
        var data = message.ProviderData;
        var userNameProviderData = data.GetData(CustomProviderData.Username);
        var passwordProviderData = data.GetData(CustomProviderData.Password);
        var orginatorProviderData = data.GetData(CustomProviderData.Orginator);

        var username = userNameProviderData.IsEmpty() ? _options.Username : userNameProviderData.GetValue<string>();
        var password = passwordProviderData.IsEmpty() ? _options.Password : passwordProviderData.GetValue<string>();
        var orginator = orginatorProviderData.IsEmpty() ? _options.Orginator : orginatorProviderData.GetValue<string>();

        var option = new IletiMerkeziMessage();

        //ToDo:detay

        return option;
    }
}