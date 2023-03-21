using MultiSms.Interfaces;
using MultiSms.SmsVitrini.Provider;
using MultiSms.SmsVitrini.Provider.Options;

namespace Microsoft.Extensions.DependencyInjection;

public static class Configurations
{
    public static MultiSmsBuilder UseSmsVitrini(this MultiSmsBuilder builder, string username, string password)
    {
        return builder.UseSmsVitrini(username, password, null);
    }

    public static MultiSmsBuilder UseSmsVitrini(this MultiSmsBuilder builder, string username, string password, string orginator)
    {
        return builder.UseSmsVitrini(options => { options.Username = username; options.Password = password; options.Orginator = orginator; });
    }

    public static MultiSmsBuilder UseSmsVitrini(this MultiSmsBuilder builder, Action<SmsVitriniProviderOptions> config)
    {
        var configuration = new SmsVitriniProviderOptions();
        config(configuration);

        configuration.Validate();

        builder.ServiceCollection.AddSingleton((s) => configuration);
        builder.ServiceCollection.AddHttpClient<ISmsProvider, SmsVitriniProvider>();
        builder.ServiceCollection.AddHttpClient<ISmsVitriniProvider, SmsVitriniProvider>();

        return builder;
    }
}