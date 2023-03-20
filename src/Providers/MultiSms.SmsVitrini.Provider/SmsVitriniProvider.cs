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
        var code = content.Result.Split('=')[1].Replace("\n", "");

        if (code == "-5")
        {
            return SendingResult.Failure(Name).AddError(new SendingError("-5", "Login hatası: Username, Password, Orginator uyumsuzluğu"));
        }
        else if (code == "-6")
        {
            return SendingResult.Failure(Name).AddError(new SendingError("-6", "Girilen bir kısım veride hata oluştu"));
        }
        else if (code == "-7")
        {
            return SendingResult.Failure(Name).AddError(new SendingError("-7", "SendDate bugünden büyük ve geçerli bir tarih olmalıdır"));
        }
        else if (code == "-8")
        {
            return SendingResult.Failure(Name).AddError(new SendingError("-8", "En azından bir Msisdn bilgisi verilmelidir"));
        }
        else if (code == "-9")
        {
            return SendingResult.Failure(Name).AddError(new SendingError("-9", "En azından bir Message değeri verilmelidir"));
        }
        else if (code == "-10")
        {
            return SendingResult.Failure(Name).AddError(new SendingError("-10", "Birden fazla Msisdn e farklı mesaj gönderimi için, Msisdn ve Message sayıları aynı olmadır"));
        }
        else if (code == "-15")
        {
            return SendingResult.Failure(Name).AddError(new SendingError("-15", "Sistem hatası"));
        }
        else if (code == "-99")
        {
            return SendingResult.Failure(Name).AddError(new SendingError("-99", "Bilinmeyen Hata"));
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