using MultiSms.Factories;
using MultiSms.Twilio.Provider.Options;

namespace MultiSms.Twilio.Provider.Extensions;

public static class TwilioComposerExtensions
{
    public static Composer UseUserName(this Composer messageComposer, string userName)
        => messageComposer.PassProviderData(CustomProviderData.Username, userName);

    public static Composer UsePassword(this Composer messageComposer, string password)
        => messageComposer.PassProviderData(CustomProviderData.Password, password);

    public static Composer UseAccountSID(this Composer messageComposer, string accountSID)
        => messageComposer.PassProviderData(CustomProviderData.AccountSID, accountSID);

    public static Composer SetPathAccountSid(this Composer messageComposer, string value)
        => messageComposer.PassProviderData(CustomProviderData.PathAccountSid, value);

    public static Composer SetMessagingServiceSid(this Composer messageComposer, string value)
        => messageComposer.PassProviderData(CustomProviderData.MessagingServiceSid, value);

    public static Composer SetMediaUrl(this Composer messageComposer, List<Uri> value)
        => messageComposer.PassProviderData(CustomProviderData.MediaUrl, value);

    public static Composer SetStatusCallback(this Composer messageComposer, Uri value)
        => messageComposer.PassProviderData(CustomProviderData.StatusCallback, value);

    public static Composer SetApplicationSid(this Composer messageComposer, string value)
        => messageComposer.PassProviderData(CustomProviderData.ApplicationSid, value);

    public static Composer SetMaxPrice(this Composer messageComposer, decimal value)
        => messageComposer.PassProviderData(CustomProviderData.MaxPrice, value);

    public static Composer SetProvideFeedback(this Composer messageComposer, bool value)
        => messageComposer.PassProviderData(CustomProviderData.ProvideFeedback, value);

    public static Composer SetAttempt(this Composer messageComposer, int value)
        => messageComposer.PassProviderData(CustomProviderData.Attempt, value);

    public static Composer SetValidityPeriod(this Composer messageComposer, int value)
        => messageComposer.PassProviderData(CustomProviderData.ValidityPeriod, value);

    public static Composer SetForceDelivery(this Composer messageComposer, bool value)
        => messageComposer.PassProviderData(CustomProviderData.ForceDelivery, value);

    public static Composer SetSmartEncoded(this Composer messageComposer, bool value)
        => messageComposer.PassProviderData(CustomProviderData.SmartEncoded, value);

    public static Composer SetPersistentAction(this Composer messageComposer, List<string> value)
        => messageComposer.PassProviderData(CustomProviderData.PersistentAction, value);

    public static Composer SetSendAsMms(this Composer messageComposer, bool value)
        => messageComposer.PassProviderData(CustomProviderData.SendAsMms, value);
}