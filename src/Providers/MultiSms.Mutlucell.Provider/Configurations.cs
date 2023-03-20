using MultiSms.Interfaces;
using MultiSms.Mutlucell.Provider;
using MultiSms.Mutlucell.Provider.Options;

namespace Microsoft.Extensions.DependencyInjection;

public static class Configurations
{
    public static MultiSmsBuilder UseMutlucell(this MultiSmsBuilder builder, string username, string password)
    {
        return builder.UseMutlucell(username, password, null);
    }

    public static MultiSmsBuilder UseMutlucell(this MultiSmsBuilder builder, string username, string password, string orginator)
    {
        return builder.UseMutlucell(options => { options.Username = username; options.Password = password; options.Orginator = orginator; });
    }

    public static MultiSmsBuilder UseMutlucell(this MultiSmsBuilder builder, Action<MutlucellProviderOptions> config)
    {
        var configuration = new MutlucellProviderOptions();
        config(configuration);

        configuration.Validate();

        builder.ServiceCollection.AddSingleton((s) => configuration);
        builder.ServiceCollection.AddHttpClient<ISmsProvider, MutlucellProvider>();
        builder.ServiceCollection.AddHttpClient<IMutlucellProvider, MutlucellProvider>();

        return builder;
    }
}