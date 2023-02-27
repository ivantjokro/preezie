using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Preezie.UnitTests
{
    public static class DbContextStubExtensions
    {
        public static IServiceCollection AddDbContextStub<TDbContext>(this IServiceCollection services) where TDbContext : DbContext
        {
            services.AddSingleton<TDbContext>(provider =>
            {
                var optionsBuilder = new DbContextOptionsBuilder<TDbContext>();
                optionsBuilder.UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString());
                return (TDbContext)Activator.CreateInstance(typeof(TDbContext), optionsBuilder.Options);
            });
            return services;
        }
    }
}