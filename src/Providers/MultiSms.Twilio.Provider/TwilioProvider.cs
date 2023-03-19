using global::Twilio;
using global::Twilio.Rest.Api.V2010.Account;
using MultiSms.Helpers;
using MultiSms.Interfaces;
using MultiSms.Models;
using MultiSms.Twilio.Provider.Options;

namespace MultiSms.Twilio.Provider;

public partial class TwilioProvider : ITwilioProvider {

  public SendingResult Send(MessageBody message) {
    try {
      CreateClient(message.ProviderData);

      var twilioMessage = CreateMessage(message);

      var result = MessageResource.Create(twilioMessage);

      return BuildResultObject(result);
    } catch (Exception ex) {
      return SendingResult.Failure(Name).AddError(ex);
    }
  }

  public async Task<SendingResult>
  SendAsync(MessageBody message,
            CancellationToken cancellationToken = default) {
    if (cancellationToken.IsCancellationRequested)
      cancellationToken.ThrowIfCancellationRequested();

    try {
      CreateClient(message.ProviderData);

      var twilioMessage = CreateMessage(message);

      var result = await MessageResource.CreateAsync(twilioMessage);

      return BuildResultObject(result);
    } catch (Exception ex) {
      return SendingResult.Failure(Name).AddError(ex);
    }
  }
}

public partial class TwilioProvider {
  public const string Name = "twilio";

  string ISmsProvider.Name => Name;

  private readonly TwilioProviderOptions _options;

  public TwilioProvider(TwilioProviderOptions options) {
    if (options is null)
      throw new ArgumentNullException(nameof(options));

    options.Validate();
    _options = options;
  }

  private void CreateClient(IEnumerable<ProviderData> data) {
    var userNameProviderData = data.GetData(CustomProviderData.Username);
    var passwordProviderData = data.GetData(CustomProviderData.Password);
    var accountSIdProviderData = data.GetData(CustomProviderData.AccountSID);

    var userName = userNameProviderData.IsEmpty()
                       ? _options.Username
                       : userNameProviderData.GetValue<string>();
    var password = passwordProviderData.IsEmpty()
                       ? _options.Password
                       : passwordProviderData.GetValue<string>();
    var accountSid = accountSIdProviderData.IsEmpty()
                         ? _options.AccountSID
                         : accountSIdProviderData.GetValue<string>();

    if (!string.IsNullOrEmpty(accountSid) &&
        !string.IsNullOrWhiteSpace(accountSid)) {
      TwilioClient.Init(userName, password, accountSid);
      return;
    }

    TwilioClient.Init(userName, password);
  }

  private static SendingResult BuildResultObject(MessageResource result) {
    if (result.Status != MessageResource.StatusEnum.Failed) {
      return SendingResult.Success(Name)
          .AddMetaData("message_id", result.Sid)
          .AddMetaData("twilio_response", result);
    }

    return SendingResult.Failure(Name).AddMetaData("twilio_response", result);
  }

  public CreateMessageOptions CreateMessage(MessageBody message) {
    var attemptChannelData =
        message.ProviderData.GetData(CustomProviderData.Attempt);
    var mediaUrlChannelData =
        message.ProviderData.GetData(CustomProviderData.MediaUrl);
    var maxPriceChannelData =
        message.ProviderData.GetData(CustomProviderData.MaxPrice);
    var sendAsMmsChannelData =
        message.ProviderData.GetData(CustomProviderData.SendAsMms);
    var smartEncodedChannelData =
        message.ProviderData.GetData(CustomProviderData.SmartEncoded);
    var forceDeliveryChannelData =
        message.ProviderData.GetData(CustomProviderData.ForceDelivery);
    var applicationSidChannelData =
        message.ProviderData.GetData(CustomProviderData.ApplicationSid);
    var statusCallbackChannelData =
        message.ProviderData.GetData(CustomProviderData.StatusCallback);
    var pathAccountSidChannelData =
        message.ProviderData.GetData(CustomProviderData.PathAccountSid);
    var validityPeriodChannelData =
        message.ProviderData.GetData(CustomProviderData.ValidityPeriod);
    var provideFeedbackChannelData =
        message.ProviderData.GetData(CustomProviderData.ProvideFeedback);
    var persistentActionChannelData =
        message.ProviderData.GetData(CustomProviderData.PersistentAction);
    var messagingServiceSidChannelData =
        message.ProviderData.GetData(CustomProviderData.MessagingServiceSid);

    var option = new CreateMessageOptions(
        new global::Twilio.Types.PhoneNumber(message.To)) {
      Body = message.Content,
      From = new global::Twilio.Types.PhoneNumber(message.Originator),
    };

    if (!pathAccountSidChannelData.IsEmpty())
      option.PathAccountSid = pathAccountSidChannelData.GetValue<string>();

    if (!messagingServiceSidChannelData.IsEmpty())
      option.MessagingServiceSid =
          messagingServiceSidChannelData.GetValue<string>();

    if (!mediaUrlChannelData.IsEmpty())
      option.MediaUrl = mediaUrlChannelData.GetValue<List<Uri>>();

    if (!statusCallbackChannelData.IsEmpty())
      option.StatusCallback = statusCallbackChannelData.GetValue<Uri>();

    if (!applicationSidChannelData.IsEmpty())
      option.ApplicationSid = applicationSidChannelData.GetValue<string>();

    if (!maxPriceChannelData.IsEmpty())
      option.MaxPrice = maxPriceChannelData.GetValue<decimal>();

    if (!provideFeedbackChannelData.IsEmpty())
      option.ProvideFeedback = provideFeedbackChannelData.GetValue<bool>();

    if (!attemptChannelData.IsEmpty())
      option.Attempt = attemptChannelData.GetValue<int>();

    if (!validityPeriodChannelData.IsEmpty())
      option.ValidityPeriod = validityPeriodChannelData.GetValue<int>();

    if (!forceDeliveryChannelData.IsEmpty())
      option.ForceDelivery = forceDeliveryChannelData.GetValue<bool>();

    if (!smartEncodedChannelData.IsEmpty())
      option.SmartEncoded = smartEncodedChannelData.GetValue<bool>();

    if (!persistentActionChannelData.IsEmpty())
      option.PersistentAction =
          persistentActionChannelData.GetValue<List<string>>();

    if (!sendAsMmsChannelData.IsEmpty())
      option.SendAsMms = sendAsMmsChannelData.GetValue<bool>();

    return option;
  }
}
