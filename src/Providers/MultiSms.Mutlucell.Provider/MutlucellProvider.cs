using System.Text;
using MultiSms.Helpers;
using MultiSms.Interfaces;
using MultiSms.Models;
using MultiSms.Mutlucell.Provider.Models;
using MultiSms.Mutlucell.Provider.Options;

namespace MultiSms.Mutlucell.Provider;

public partial class MutlucellProvider : IMutlucellProvider
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

            using var request = new HttpRequestMessage(HttpMethod.Post, new UriBuilder(_options.BaseUrl) { Path = "smsgw-ws/sndblke" }.Uri);
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

public partial class MutlucellProvider
{
    private readonly HttpClient _httpClient;
    private readonly MutlucellProviderOptions _options;

    string ISmsProvider.Name => Name;

    public const string Name = "Mutlucell";

    public MutlucellProvider(HttpClient httpClient, MutlucellProviderOptions options)
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

        if (code == "20")
        {
            return SendingResult.Failure(Name).AddError(new SendingError("20", "Xml eksik veya hatalı."));
        }
        else if (code == "21")
        {
            return SendingResult.Failure(Name).AddError(new SendingError("21", "Kullanılan originatöre sahip değilsiniz."));
        }
        else if (code == "22")
        {
            return SendingResult.Failure(Name).AddError(new SendingError("22", "Kontörünüz yetersiz."));
        }
        else if (code == "23")
        {
            return SendingResult.Failure(Name).AddError(new SendingError("23", "Kullanıcı adı ya da parolanız hatalı."));
        }
        else if (code == "24")
        {
            return SendingResult.Failure(Name).AddError(new SendingError("24", "Şu anda size ait başka bir işlem aktif."));
        }
        else if (code == "25")
        {
            return SendingResult.Failure(Name).AddError(new SendingError("25", "SMSC Stopped (Bu hatayı alırsanız, işlemi 1-2 dk sonra tekrar deneyin)"));
        }
        else if (code == "30")
        {
            return SendingResult.Failure(Name).AddError(new SendingError("30", "Hesap Aktivasyonu sağlanmamış"));
        }
        else
        {
            return SendingResult.Success(Name).AddMetaData("response", result);
        }
    }

    public MutlucellMessage CreateMessage(MessageBody message)
    {
        var data = message.ProviderData;
        var userNameProviderData = data.GetData(CustomProviderData.Username);
        var passwordProviderData = data.GetData(CustomProviderData.Password);
        var orginatorProviderData = data.GetData(CustomProviderData.Orginator);

        var username = userNameProviderData.IsEmpty() ? _options.Username : userNameProviderData.GetValue<string>();
        var password = passwordProviderData.IsEmpty() ? _options.Password : passwordProviderData.GetValue<string>();
        var orginator = orginatorProviderData.IsEmpty() ? _options.Orginator : orginatorProviderData.GetValue<string>();

        var option = new MutlucellMessage
        {
            Ka = username,
            Pwd = password,
            Org = orginator,
            Charset = "turkish",
            Mesaj = new List<Mesaj>
            {
                new Mesaj
                {
                    Metin = message.Content,
                    Nums = message.To
                }
            }
        };

        return option;
    }
}