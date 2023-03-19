using MultiSms.Models;

namespace Microsoft.Extensions.DependencyInjection;

public class MultiSmsBuilder {
  public MultiSmsBuilder(IServiceCollection serviceCollection,
                         MultiSmsServiceOptions configuration) {
    Configuration = configuration;
    ServiceCollection = serviceCollection;
  }

  public IServiceCollection ServiceCollection { get; }

  public MultiSmsServiceOptions Configuration { get; }
}
