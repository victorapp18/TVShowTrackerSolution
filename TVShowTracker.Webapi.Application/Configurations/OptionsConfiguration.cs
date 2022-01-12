using TVShowTracker.Webapi.Application.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace TVShowTracker.Webapi.Application.Configurations
{
    public static class OptionsConfiguration
    {
        public static void ConfigureOptions(this IServiceCollection services, IConfiguration configuration) 
        {
            services.Configure<AppKeysOption>(options => configuration.GetSection("AppKeys").Bind(options));
            services.Configure<SendGridOption>(options => configuration.GetSection("sendGrid").Bind(options));
            services.Configure<SpreadsheetS3Options>(options => configuration.GetSection("spreadsheetS3").Bind(options));
        }
    }
}
