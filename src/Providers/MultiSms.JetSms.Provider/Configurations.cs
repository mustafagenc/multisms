using MultiSms.Interfaces;
using MultiSms.JetSms.Provider;
using MultiSms.JetSms.Provider.Options;

namespace Microsoft.Extensions.DependencyInjection;

public static class Configurations
{
    public static MultiSmsBuilder UseJetSms(this MultiSmsBuilder builder, string username, string password)
    {
        return builder.UseJetSms(username, password, null);
    }

    public static MultiSmsBuilder UseJetSms(this MultiSmsBuilder builder, string username, string password, string orginator)
    {
        return builder.UseJetSms(options => { options.Username = username; options.Password = password; options.Orginator = orginator; });
    }

    public static MultiSmsBuilder UseJetSms(this MultiSmsBuilder builder, Action<JetSmsProviderOptions> config)
    {
        var configuration = new JetSmsProviderOptions();
        config(configuration);

        configuration.Validate();

        builder.ServiceCollection.AddSingleton((s) => configuration);
        builder.ServiceCollection.AddHttpClient<ISmsProvider, JetSmsProvider>();
        builder.ServiceCollection.AddHttpClient<IJetSmsProvider, JetSmsProvider>();

        return builder;
    }
}