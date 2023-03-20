using MultiSms.Factories;
using MultiSms.Mutlucell.Provider.Options;

namespace MultiSms.Mutlucell.Provider.Extensions;

public static class MutlucellMultiSmsServiceFactoryExtensions
{
    public static MultiSmsServiceFactory UseMutlucell(this MultiSmsServiceFactory builder, string username, string password, string orginator)
    {
        return builder.UseMutlucell(options => { options.Username = username; options.Password = password; options.Orginator = orginator; });
    }

    public static MultiSmsServiceFactory UseMutlucell(this MultiSmsServiceFactory builder, Action<MutlucellProviderOptions> config)
    {
        var configuration = new MutlucellProviderOptions();
        config(configuration);
        configuration.Validate();
        builder.UseProvider(new MutlucellProvider(null, configuration));
        return builder;
    }
}
