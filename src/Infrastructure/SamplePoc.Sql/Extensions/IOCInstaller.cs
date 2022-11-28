using Microsoft.Extensions.DependencyInjection;
using SamplePoc.Services.Abstraction;

namespace SamplePoc.Sql.Extensions
{
    public static class IOCInstaller
    {
        public static IServiceCollection AddSql(this IServiceCollection services)
        {
            services.AddDbContext<MarketingSuiteContext>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }
    }
}
