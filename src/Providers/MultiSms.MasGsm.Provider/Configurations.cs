using MultiSms.Interfaces;
using MultiSms.MasGsm.Provider;
using MultiSms.MasGsm.Provider.Options;

namespace Microsoft.Extensions.DependencyInjection;

public static class Configurations
{
    public static MultiSmsBuilder UseMasGsm(this MultiSmsBuilder builder, string username, string password)
    {
        return builder.UseMasGsm(username, password, null);
    }

    public static MultiSmsBuilder UseMasGsm(this MultiSmsBuilder builder, string username, string password, string orginator)
    {
        return builder.UseMasGsm(options => { options.Username = username; options.Password = password; options.Orginator = orginator; });
    }

    public static MultiSmsBuilder UseMasGsm(this MultiSmsBuilder builder, Action<MasGsmProviderOptions> config)
    {
        var configuration = new MasGsmProviderOptions();
        config(configuration);

        configuration.Validate();

        builder.ServiceCollection.AddSingleton((s) => configuration);
        builder.ServiceCollection.AddHttpClient<ISmsProvider, MasGsmProvider>();
        builder.ServiceCollection.AddHttpClient<IMasGsmProvider, MasGsmProvider>();

        return builder;
    }
}