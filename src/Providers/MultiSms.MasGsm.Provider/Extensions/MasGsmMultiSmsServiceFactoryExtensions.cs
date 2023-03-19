using MultiSms.Factories;
using MultiSms.MasGsm.Provider.Options;

namespace MultiSms.MasGsm.Provider.Extensions;

public static class MasGsmMultiSmsServiceFactoryExtensions
{
    public static MultiSmsServiceFactory UseMasGsm(this MultiSmsServiceFactory builder, string username, string password, string orginator)
    {
        return builder.UseMasGsm(options => { options.Username = username; options.Password = password; options.Orginator = orginator; });
    }

    public static MultiSmsServiceFactory UseMasGsm(this MultiSmsServiceFactory builder, Action<MasGsmProviderOptions> config)
    {
        var configuration = new MasGsmProviderOptions();
        config(configuration);
        configuration.Validate();
        builder.UseProvider(new MasGsmProvider(null, configuration));
        return builder;
    }
}