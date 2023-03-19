using MultiSms.Factories;
using MultiSms.JetSms.Provider.Options;

namespace MultiSms.JetSms.Provider.Extensions;

public static class JetSmsMultiSmsServiceFactoryExtensions
{
    public static MultiSmsServiceFactory UseJetSms(this MultiSmsServiceFactory builder, string username, string password, string orginator)
    {
        return builder.UseJetSms(options => { options.Username = username; options.Password = password; options.Orginator = orginator; });
    }

    public static MultiSmsServiceFactory UseJetSms(this MultiSmsServiceFactory builder, Action<JetSmsProviderOptions> config)
    {
        var configuration = new JetSmsProviderOptions();
        config(configuration);
        configuration.Validate();
        builder.UseProvider(new JetSmsProvider(null, configuration));
        return builder;
    }
}