using System.Web;
using MultiSms.Helpers;
using MultiSms.Interfaces;
using MultiSms.JetSms.Provider.Models;
using MultiSms.JetSms.Provider.Options;
using MultiSms.Models;

namespace MultiSms.JetSms.Provider;

public partial class JetSmsProvider : IJetSmsProvider {
  public SendingResult Send(MessageBody message) {
    return SendAsync(message).GetAwaiter().GetResult();
  }

  public async Task<SendingResult>
  SendAsync(MessageBody message,
            CancellationToken cancellationToken = default) {
    try {
      var client = CreateClient();

      using var request =
          new HttpRequestMessage(HttpMethod.Post, CreateUrl(message));
      using var response =
          await client
              .SendAsync(request, HttpCompletionOption.ResponseHeadersRead,
                         cancellationToken)
              .ConfigureAwait(false);

      return BuildResultObject(response);
    } catch (Exception ex) {
      return SendingResult.Failure(Name).AddError(ex);
    }
  }
}

public partial class JetSmsProvider {
  private readonly HttpClient _httpClient;
  private readonly JetSmsProviderOptions _options;

  string ISmsProvider.Name => Name;

  public const string Name = "JetSms";

  public JetSmsProvider(HttpClient httpClient, JetSmsProviderOptions options) {
    if (options is null)
      throw new ArgumentNullException(nameof(options));

    options.Validate();
    _options = options;

    _httpClient = httpClient ?? new HttpClient();
  }

  private HttpClient CreateClient() => _httpClient;

  private static SendingResult BuildResultObject(HttpResponseMessage result) {
    using var content = result.Content.ReadAsStringAsync();
    var code = content.Result.Split('=') [1].Replace("\n", "");
    var error = JetSmsMessageResponse.Result(code);

    if (!string.IsNullOrEmpty(error)) {
      return SendingResult.Failure(Name).AddError(
          new SendingError(code, error));
    } else {
      return SendingResult.Success(Name).AddMetaData("response", result);
    }
  }

  public Uri CreateUrl(MessageBody message) {
    var data = message.ProviderData;
    var userNameProviderData = data.GetData(CustomProviderData.Username);
    var passwordProviderData = data.GetData(CustomProviderData.Password);
    var orginatorProviderData = data.GetData(CustomProviderData.Orginator);

    var username = userNameProviderData.IsEmpty()
                       ? _options.Username
                       : userNameProviderData.GetValue<string>();
    var password = passwordProviderData.IsEmpty()
                       ? _options.Password
                       : passwordProviderData.GetValue<string>();
    var orginator = orginatorProviderData.IsEmpty()
                        ? _options.Orginator
                        : orginatorProviderData.GetValue<string>();

    var builder =
        new UriBuilder(_options.BaseUrl) { Path = "SMS-Web/HttpSmsSend" };

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
