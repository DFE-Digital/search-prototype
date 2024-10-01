using Microsoft.Extensions.Configuration;

namespace Dfe.Data.SearchPrototype.Tests.Integration.TestHarness;

public class ConfigBuilder
{
    public IConfiguration SetupConfiguration(Dictionary<string, string?> options)
    {
        var configBuilder = new ConfigurationBuilder();

        configBuilder.AddInMemoryCollection(options);
        return configBuilder.Build();
    }
}
