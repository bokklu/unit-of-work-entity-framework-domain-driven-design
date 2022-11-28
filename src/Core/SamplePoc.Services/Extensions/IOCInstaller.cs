using Microsoft.Extensions.DependencyInjection;
using SamplePoc.Services.Abstraction;

namespace SamplePoc.Services.Extensions
{
    public static class IOCInstaller
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<ICampaignService, CampaignService>();
            services.AddScoped<IKeywordService, KeywordService>();

            return services;
        }
    }
}
