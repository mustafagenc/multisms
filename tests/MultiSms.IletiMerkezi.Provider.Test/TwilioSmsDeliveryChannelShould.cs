using MultiSms.Exceptions;
using MultiSms.Models;
using MultiSms.Twilio.Provider;
using MultiSms.Twilio.Provider.Extensions;
using MultiSms.Twilio.Provider.Options;
using Xunit;

namespace MultiSms.Provider.Test;

public class TwilioProviderShould {
  static readonly string TEST_ORGINATOR =
      EnvironmentVariable.Load("TWILIO_ORGINATOR");
  static readonly string TEST_TO = EnvironmentVariable.Load("TWILIO_TO");
  static readonly string TEST_USERNAME =
      EnvironmentVariable.Load("TWILIO_USERNAME");
  static readonly string TEST_PASSWORD =
      EnvironmentVariable.Load("TWILIO_PASSWORD");

  [Fact]
  public void ThrowIfOptionsIsNull() {
    TwilioProviderOptions options = null;

    Assert.Throws<ArgumentNullException>(
        () => { new TwilioProvider(options); });
  }

  [Fact]
  public void ThrowIfOptionsNotValid_AllIsNull() {
    var options = new TwilioProviderOptions();

    Assert.Throws<RequiredOptionException<TwilioProviderOptions>>(
        () => { new TwilioProvider(options); });
  }

  [Fact]
  public void ThrowIfOptionsNotValid_Username_IsNull() {
    var options = new TwilioProviderOptions() { Password = TEST_PASSWORD };

    Assert.Throws<RequiredOptionException<TwilioProviderOptions>>(
        () => { new TwilioProvider(options); });
  }

  [Fact]
  public void ThrowIfOptionsNotValid_Username_IsEmpty() {
    var options =
        new TwilioProviderOptions() { Username = "", Password = TEST_PASSWORD };

    Assert.Throws<RequiredOptionException<TwilioProviderOptions>>(
        () => { new TwilioProvider(options); });
  }

  [Fact]
  public void ThrowIfOptionsNotValid_Password_IsNull() {
    var options = new TwilioProviderOptions() { Username = TEST_USERNAME };

    Assert.Throws<RequiredOptionException<TwilioProviderOptions>>(
        () => { new TwilioProvider(options); });
  }

  [Fact]
  public void ThrowIfOptionsNotValid_Password_IsEmpty() {
    var options =
        new TwilioProviderOptions() { Password = "", Username = TEST_USERNAME };

    Assert.Throws<RequiredOptionException<TwilioProviderOptions>>(
        () => { new TwilioProvider(options); });
  }

  [Fact]
  public void CreatSmsMessageFromMessage() {
    var channel = new TwilioProvider(
        new TwilioProviderOptions() { Password = TEST_PASSWORD,
                                      Username = TEST_USERNAME });

    var message = MessageBody.Compose()
                      .Orginator(TEST_ORGINATOR)
                      .To(TEST_TO)
                      .WithContent("test message")
                      .Build();

    var smsMessage = channel.CreateMessage(message);

    Assert.Equal(message.Originator.ToString(), smsMessage.From.ToString());
    Assert.Equal(message.To.ToString(), smsMessage.To.ToString());
    Assert.Equal(message.Content, smsMessage.Body);
  }

  [Fact]
  public void CreatSmsMessageFromMessageWithCustomData() {
    var provider = new TwilioProvider(
        new TwilioProviderOptions() { Password = TEST_PASSWORD,
                                      Username = TEST_USERNAME });

    var expectedPersistentAction = new List<string> { "value_1" };
    var expectedStatusCallback = new Uri("https://example.com/webhook");
    var expectedMediaUrl =
        new List<Uri> { new Uri("https://example.com/logo.png") };

    var messageComposer =
        MessageBody.Compose()
            .To(TEST_TO)
            .Orginator(TEST_ORGINATOR)
            .WithContent("test message")
            .SetAttempt(1)
            .SetMaxPrice(12.2m)
            .SetSendAsMms(true)
            .SetValidityPeriod(1)
            .SetSmartEncoded(true)
            .SetForceDelivery(true)
            .SetProvideFeedback(true)
            .SetMediaUrl(expectedMediaUrl)
            .SetApplicationSid("application_sid_value")
            .SetPersistentAction(expectedPersistentAction)
            .SetMessagingServiceSid("messaging_service_sid_value");

    TwilioComposerExtensions.SetStatusCallback(messageComposer,
                                               expectedStatusCallback);

    var message = messageComposer.Build();
    var smsMessage = provider.CreateMessage(message);

    Assert.True(smsMessage.SendAsMms);
    Assert.Equal(1, smsMessage.Attempt);
    Assert.True(smsMessage.SmartEncoded);
    Assert.True(smsMessage.ForceDelivery);
    Assert.True(smsMessage.ProvideFeedback);
    Assert.Equal(12.2m, smsMessage.MaxPrice);
    Assert.Equal(1, smsMessage.ValidityPeriod);
    Assert.Equal(expectedMediaUrl, smsMessage.MediaUrl);
    Assert.Equal(expectedStatusCallback, smsMessage.StatusCallback);
    Assert.Equal("application_sid_value", smsMessage.ApplicationSid);
    Assert.Equal(expectedPersistentAction, smsMessage.PersistentAction);
    Assert.Equal("messaging_service_sid_value", smsMessage.MessagingServiceSid);
  }

  [Fact(Skip = "no auth keys")]
  public void SendSMS() {
    var provider = new TwilioProvider(
        new TwilioProviderOptions() { Password = TEST_PASSWORD,
                                      Username = TEST_USERNAME });

    var message = MessageBody.Compose()
                      .Orginator(TEST_ORGINATOR)
                      .To(TEST_TO)
                      .WithContent("test message")
                      .Build();

    var result = provider.Send(message);

    Assert.True(result.IsSuccess);
  }
}