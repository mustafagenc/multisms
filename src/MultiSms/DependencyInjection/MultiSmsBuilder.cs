using Microsoft.Extensions.DependencyInjection;
using MultiSms.Models;

namespace MultiSms.DependencyInjection;

/// <summary>
/// 
/// </summary>
public class MultiSmsBuilder
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="serviceCollection"></param>
    /// <param name="configuration"></param>
    public MultiSmsBuilder(IServiceCollection serviceCollection, MultiSmsServiceOptions configuration)
    {
        Configuration = configuration;
        ServiceCollection = serviceCollection;
    }

    /// <summary>
    /// 
    /// </summary>
    public IServiceCollection ServiceCollection { get; }

    /// <summary>
    /// 
    /// </summary>
    public MultiSmsServiceOptions Configuration { get; }

}