using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Preezie.Domain;
using Preezie.Presentation.Controllers;

namespace Preezie.UnitTests.Fixtures
{
    public class UserControllerFixture
    {
        private Action<IServiceCollection> _setup;

        public UserControllerFixture()
        {
            _setup = c => { c.AddTransient<UserController>(); };
        }

        public UserController CreateSut(IConfiguration configuration)
        {
            var services = new ServiceCollection()
                .AddSingleton(configuration)
                .RegisterCoreServices()
                .AddLogging();

            // Use AddCleanDbContextStub instead of AddDbContextStub
            services.AddDbContextStub<UserDbContext>();

            IServiceProvider sp = new TestServiceProvider()
                .CreateProvider(services, _setup);

            using IServiceScope scope = sp.CreateScope();

            return scope
                .ServiceProvider
                .GetService<UserController>();
        }

        public UserControllerFixture WithLogger<T>(ILogger<T> logger)
        {
            _setup += services => services.AddSingleton(logger);

            return this;
        }

        public UserControllerFixture WithUserContextDb()
        {
            _setup += services => services.AddDbContextStub<UserDbContext>();

            return this;
        }
    }
}