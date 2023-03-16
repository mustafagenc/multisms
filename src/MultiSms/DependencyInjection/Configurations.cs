using Microsoft.Extensions.DependencyInjection;
using MultiSms.Interfaces;
using MultiSms.Models;

namespace MultiSms.DependencyInjection;

public static class Configurations
{
    public static MultiSmsBuilder AddMultiSms(this IServiceCollection serviceCollection, string defaultProviderName)
        => AddMultiSms(serviceCollection, options => options.DefaultProvider = defaultProviderName);

    public static MultiSmsBuilder AddMultiSms(this IServiceCollection serviceCollection, Action<MultiSmsServiceOptions> config)
    {
        if (config is null)
            throw new ArgumentNullException(nameof(config));

        var configuration = new MultiSmsServiceOptions();
        config(configuration);

        serviceCollection.AddSingleton((s) => configuration);
        serviceCollection.AddScoped<ISmsService, SmsService>();

        return new MultiSmsBuilder(serviceCollection, configuration);
    }
}