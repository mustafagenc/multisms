using MultiSms.Factories;
using MultiSms.NetGsm.Provider.Options;

namespace MultiSms.NetGsm.Provider.Extensions;

public static class NetGsmMultiSmsServiceFactoryExtensions
{
    public static MultiSmsServiceFactory UseNetGsm(this MultiSmsServiceFactory builder, string username, string password, string orginator)
    {
        return builder.UseNetGsm(options => { options.Username = username; options.Password = password; options.Orginator = orginator; });
    }

    public static MultiSmsServiceFactory UseNetGsm(this MultiSmsServiceFactory builder, Action<NetGsmProviderOptions> config)
    {
        var configuration = new NetGsmProviderOptions();
        config(configuration);
        configuration.Validate();
        builder.UseProvider(new NetGsmProvider(null, configuration));
        return builder;
    }
}