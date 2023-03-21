using System.Web;
using MultiSms.Helpers;
using MultiSms.Interfaces;
using MultiSms.Models;
using MultiSms.SmsVitrini.Provider.Options;

namespace MultiSms.SmsVitrini.Provider;

public partial class SmsVitriniProvider : ISmsVitriniProvider
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

            using var request = new HttpRequestMessage(HttpMethod.Post, CreateUrl(message));
            using var response = await client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, cancellationToken).ConfigureAwait(false);

            return BuildResultObject(response);
        }
        catch (Exception ex)
        {
            return SendingResult.Failure(Name).AddError(ex);
        }
    }
}

public partial class SmsVitriniProvider
{
    private readonly HttpClient _httpClient;
    private readonly SmsVitriniProviderOptions _options;

    string ISmsProvider.Name => Name;

    public const string Name = "SmsVitrini";

    public SmsVitriniProvider(HttpClient httpClient, SmsVitriniProviderOptions options)
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

        if (code == "-1")
        {
            return SendingResult.Failure(Name).AddError(new SendingError("-5", "Login hatası: Username, Password, Orginator uyumsuzluğu"));
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
        builder.Path = "SMS-Web/HttpSmsSend";

        var query = HttpUtility.ParseQueryString(builder.Query);
        query["Username"] = username;
        query["Password"] = password;
        query["Originator"] = orginator;
        query["Msisdns"] = message.To.ToString();
        query["Messages"] = message.Content;

        builder.Query = query.ToString();

        return builder.Uri;
    }
}