using Microsoft.Extensions.Configuration;

namespace Preezie.UnitTests
{
    public static class UnitTestFactory
    {
        public static IConfiguration CreateTestConfiguration(Dictionary<string, string> settings)
        {
            IConfiguration configuration = new ConfigurationBuilder()
                .AddEnvironmentVariables()
                .AddInMemoryCollection(settings)
                .Build();

            return configuration;
        }
    }
}