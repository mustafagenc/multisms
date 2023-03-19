using MultiSms.Factories;
using MultiSms.SmsVitrini.Provider.Options;

namespace MultiSms.SmsVitrini.Provider.Extensions;

public static class SmsVitriniMultiSmsServiceFactoryExtensions
{
    public static MultiSmsServiceFactory UseSmsVitrini(this MultiSmsServiceFactory builder, string username, string password, string orginator)
    {
        return builder.UseSmsVitrini(options => { options.Username = username; options.Password = password; options.Orginator = orginator; });
    }

    public static MultiSmsServiceFactory UseSmsVitrini(this MultiSmsServiceFactory builder, Action<SmsVitriniProviderOptions> config)
    {
        var configuration = new SmsVitriniProviderOptions();
        config(configuration);
        configuration.Validate();
        builder.UseProvider(new SmsVitriniProvider(null, configuration));
        return builder;
    }
}