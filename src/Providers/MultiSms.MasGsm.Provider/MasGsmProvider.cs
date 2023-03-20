using MultiSms.Helpers;
using MultiSms.Interfaces;
using MultiSms.MasGsm.Provider.Options;
using MultiSms.Models;
using System.Web;

namespace MultiSms.MasGsm.Provider;

public partial class MasGsmProvider : IMasGsmProvider
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

            //ToDo: sorun var duzenlecek
            //https://www.masgsm.com.tr/files/api-dokumantasyonu.pdf
            using var request = new HttpRequestMessage(HttpMethod.Get, CreateUrl(message));
            using var response = await client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, cancellationToken).ConfigureAwait(false);

            return BuildResultObject(response);
        }
        catch (Exception ex)
        {
            return SendingResult.Failure(Name).AddError(ex);
        }
    }
}

public partial class MasGsmProvider
{
    private readonly HttpClient _httpClient;
    private readonly MasGsmProviderOptions _options;

    string ISmsProvider.Name => Name;

    public const string Name = "MasGsm";

    public MasGsmProvider(HttpClient httpClient, MasGsmProviderOptions options)
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
        using var content = result.Content.ReadAsStringAsync();
        var code = content.Result;

        if (code == "93")
        {
            return SendingResult.Failure(Name).AddError(new SendingError("93", "Bilinmeyen hata..."));
        }
        else
        {
            return SendingResult.Success(Name).AddMetaData("response", result);
        }
    }

    public Uri CreateUrl(MessageBody message)
    {
        var data = message.ProviderData;
        var userNameProviderData = data.GetData(CustomProviderData.Username);
        var passwordProviderData = data.GetData(CustomProviderData.Password);
        var orginatorProviderData = data.GetData(CustomProviderData.Orginator);

        var username = userNameProviderData.IsEmpty() ? _options.Username : userNameProviderData.GetValue<string>();
        var password = passwordProviderData.IsEmpty() ? _options.Password : passwordProviderData.GetValue<string>();
        var orginator = orginatorProviderData.IsEmpty() ? _options.Orginator : orginatorProviderData.GetValue<string>();

        var builder = new UriBuilder(_options.BaseUrl);
        builder.Path = "smsget/v1";

        var query = HttpUtility.ParseQueryString(builder.Query);
        query["username"] = username;
        query["password"] = password;
        query["header"] = orginator;

        query["gsm"] = message.To.ToString();
        query["message"] = message.Content;

        builder.Query = query.ToString();

        return builder.Uri;
    }
}