using Microsoft.Extensions.DependencyInjection;

namespace Preezie.UnitTests
{
    public class TestServiceProvider
    {
        public IServiceProvider CreateProvider(
            IServiceCollection services,
            Action<IServiceCollection> overrides = null)
        {
            overrides?.Invoke(services);

            return services.BuildServiceProvider(new ServiceProviderOptions
            {
                ValidateOnBuild = true,
                ValidateScopes = true
            });
        }
    }
}