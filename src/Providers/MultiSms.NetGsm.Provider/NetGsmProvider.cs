using System.Text;
using System.Xml.Serialization;
using MultiSms.Interfaces;
using MultiSms.Models;
using MultiSms.NetGsm.Provider.Models;
using MultiSms.NetGsm.Provider.Options;
using MultiSms.Helpers;

namespace MultiSms.NetGsm.Provider;

public partial class NetGsmProvider : INetGsmProvider
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
        var data = message.ProviderData;
        var userNameProviderData = data.GetData(CustomProviderData.Username);
        var passwordProviderData = data.GetData(CustomProviderData.Password);
        var orginatorProviderData = data.GetData(CustomProviderData.Orginator);

        var username = userNameProviderData.IsEmpty() ? _options.Username : userNameProviderData.GetValue<string>();
        var password = passwordProviderData.IsEmpty() ? _options.Password : passwordProviderData.GetValue<string>();
        var orginator = orginatorProviderData.IsEmpty() ? _options.Orginator : orginatorProviderData.GetValue<string>();

        var option = new NetGsmMessage();
        option.Header = new Header
        {
            Company = new Company { Dil = "TR", Text = "Netgsm" },
            Usercode = username,
            Password = password,
            Type = "1:n",
            Msgheader = orginator
        };

        option.Body = new Body
        {
            Msg = message.Content,
            No = message.To
        };

        return option;
    }
}