using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using SamplePoc.Contracts.Request;
using SamplePoc.Host.Validators;
using SamplePoc.Services.Extensions;
using SamplePoc.Sql.Extensions;
using SamplePoc.Sql.Options;

namespace SamplePoc.Host.Extensions
{
    public static class UseExtensions
    {
        public static WebApplicationBuilder AddApi(this WebApplicationBuilder builder)
        {
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddScoped<IValidator<CampaignAddRequest>, CampaignAddRequestValidator>();
            builder.Services.AddScoped<IValidator<CampaignBulkAddRequest>, CampaignBulkAddRequestValidator>();
            builder.Services.AddScoped<IValidator<CampaignUpdateRequest>, CampaignUpdateRequestValidator>();
            builder.Services.AddScoped<IValidator<KeywordAddRequest>, KeywordAddRequestValidator>();
            builder.Services.AddScoped<IValidator<KeywordBulkAddRequest>, KeywordBulkAddRequestValidator>();
            builder.Services.AddScoped<IValidator<KeywordUpdateRequest>, KeywordUpdateRequestValidator>();

            return builder;
        }

        public static WebApplicationBuilder AddDomain(this WebApplicationBuilder builder)
        {
            builder.Services.AddSql();
            builder.Services.AddServices();

            return builder;
        }

        public static WebApplicationBuilder AddOptions(this WebApplicationBuilder builder)
        {
            builder.Services.ConfigureOptions<SqlConfigureOptions>();

            return builder;
        }
    }
}
