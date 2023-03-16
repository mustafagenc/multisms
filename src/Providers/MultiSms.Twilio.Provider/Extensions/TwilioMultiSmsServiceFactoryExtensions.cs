using MultiSms.Factories;
using MultiSms.Twilio.Provider.Options;

namespace MultiSms.Twilio.Provider.Extensions;

public static class TwilioMultiSmsServiceFactoryExtensions
{
 
    public static MultiSmsServiceFactory UseTwilio(this MultiSmsServiceFactory builder, string username, string password)
        => builder.UseTwilio(username, password, null);

    public static MultiSmsServiceFactory UseTwilio(this MultiSmsServiceFactory builder, string username, string password, string accountSID)
       => builder.UseTwilio(op => { op.Username = username; op.Password = password; op.AccountSID = accountSID; });

    public static MultiSmsServiceFactory UseTwilio(this MultiSmsServiceFactory builder, Action<TwilioProviderOptions> config)
    {
        var configuration = new TwilioProviderOptions();
        config(configuration);
        configuration.Validate();
        builder.UseProvider(new TwilioProvider(configuration));

        return builder;
    }
}