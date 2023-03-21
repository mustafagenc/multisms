using MultiSms.Factories;
using MultiSms.IletiMerkezi.Provider.Options;

namespace MultiSms.IletiMerkezi.Provider.Extensions;

public static class IletiMerkeziMultiSmsServiceFactoryExtensions
{
    public static MultiSmsServiceFactory UseIletiMerkezi(this MultiSmsServiceFactory builder, string key, string hash, string orginator)
    {
        return builder.UseIletiMerkezi(options => { options.Key = key; options.Hash = hash; options.Orginator = orginator; });
    }

    public static MultiSmsServiceFactory UseIletiMerkezi(this MultiSmsServiceFactory builder, Action<IletiMerkeziProviderOptions> config)
    {
        var configuration = new IletiMerkeziProviderOptions();
        config(configuration);
        configuration.Validate();
        builder.UseProvider(new IletiMerkeziProvider(null, configuration));
        return builder;
    }
}