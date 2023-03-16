using System.Text;
using MultiSms.Interfaces;
using MultiSms.Models;
using MultiSms.NetGsm.Provider.Models;
using MultiSms.NetGsm.Provider.Options;
using Newtonsoft.Json;

namespace MultiSms.NetGsm.Provider;

public partial class NetGsmProvider : INetGsmProvider
{
    public SendingResult Send(MessageBody message)
        => SendAsync(message).GetAwaiter().GetResult();

    public async Task<SendingResult> SendAsync(MessageBody message, CancellationToken cancellationToken = default)
    {
        try
        {
            var client = CreateClient();

            using var request = new HttpRequestMessage(HttpMethod.Post, new UriBuilder(_options.BaseUrl) { Path = "sms/send/xml" }.Uri);

            using var jsonContent = new StringContent(JsonConvert.SerializeObject(CreateMessage(message)), Encoding.UTF8, "application/json");

            request.Content = jsonContent;

            using var response = await client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, cancellationToken).ConfigureAwait(false);

            return BuildResultObject(response);
        }
        catch (Exception ex)
        {
            return SendingResult.Failure(Name).AddError(ex);
        }
    }
}

public partial class NetGsmProvider
{
    private readonly HttpClient _httpClient;
    private readonly NetGsmProviderOptions _options;

    string ISmsProvider.Name => Name;

    public const string Name = "NetGsm";

    public NetGsmProvider(HttpClient httpClient, NetGsmProviderOptions options)
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

    public NetGsmMessage CreateMessage(MessageBody message)
    {
        var option = new NetGsmMessage()
        {
            Phone = message.To,
            Orginator = message.Originator,
            Message = message.Content,
        };


        return option;
    }
}