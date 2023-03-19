using MultiSms.Factories;
using MultiSms.IletiMerkezi.Provider.Options;

namespace MultiSms.IletiMerkezi.Provider.Extensions;

public static class IletiMerkeziMultiSmsServiceFactoryExtensions
{
    public static MultiSmsServiceFactory UseNetGsm(this MultiSmsServiceFactory builder, string username, string password, string orginator)
    {
        return builder.UseNetGsm(options => { options.Username = username; options.Password = password; options.Orginator = orginator; });
    }

    public static MultiSmsServiceFactory UseNetGsm(this MultiSmsServiceFactory builder, Action<IletiMerkeziProviderOptions> config)
    {
        var configuration = new IletiMerkeziProviderOptions();
        config(configuration);
        configuration.Validate();
        builder.UseProvider(new IletiMerkeziProvider(null, configuration));
        return builder;
    }
}