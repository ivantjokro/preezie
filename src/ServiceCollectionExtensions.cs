using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Preezie.Application;
using Preezie.Domain;
using Preezie.Domain.Repositories;
using Preezie.Infrastructure;

namespace Preezie
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection RegisterDatabaseContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<ApiSettings>(configuration.GetSection(nameof(ApiSettings)));

            IOptions<ApiSettings> apiSettings = services.BuildServiceProvider().GetRequiredService<IOptions<ApiSettings>>();

            services.AddDbContext<UserDbContext>(options => options.UseSqlServer($"Server={apiSettings.Value.ServerName};Database={apiSettings.Value.DatabaseName};User Id={apiSettings.Value.UserId};Password={apiSettings.Value.Password};TrustServerCertificate=true"))
                .BuildServiceProvider();

            return services;
        }

        public static IServiceCollection RegisterCoreServices(this IServiceCollection services)
        {
            services.AddTransient<IUserMediator, UserMediator>();
            services.AddTransient<IUserRepository, UserRepository>();

            return services;
        }
    }
}
