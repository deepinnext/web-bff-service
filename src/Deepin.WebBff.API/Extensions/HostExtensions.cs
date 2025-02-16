using Deepin.Infrastructure;
using Deepin.Infrastructure.Caching;
using Deepin.Infrastructure.Extensions;
using Deepin.ServiceDefaults.Extensions;
using Deepin.WebBff.API.ApiClients;
using Deepin.WebBff.API.Configurations;

namespace Deepin.WebBff.API.Extensions;

public static class HostExtensions
{
    public static WebApplicationBuilder AddApplicationService(this WebApplicationBuilder builder)
    {
        builder.AddServiceDefaults();

        builder.Services.Configure<UrlsConfig>(builder.Configuration.GetSection("Urls"));

        builder.Services
        .AddDefaultCache(new RedisCacheOptions
        {
            ConnectionString = builder.Configuration.GetConnectionString("RedisConnection") ?? throw new ArgumentNullException("RedisConnection")
        })
        .AddDefaultUserContexts()
        .AddApiClients();

        return builder;
    }
    public static WebApplication UseApplicationService(this WebApplication app)
    {
        app.UseServiceDefaults();

        app.MapGet("/api/about", () => new
        {
            Name = "Deepin.WebBff.API",
            Version = "1.0.0",
            DeepinEnv = app.Configuration["DEEPIN_ENV"],
            app.Environment.EnvironmentName
        });
        return app;
    }
    private static IServiceCollection AddApiClients(this IServiceCollection services)
    {
        services.AddTransient<HttpClientAuthorizationDelegatingHandler>();
        services.AddHttpClient<IChatApiClient, ChatApiClient>().AddDefaultPolicies();
        services.AddHttpClient<IIdentityApiClient, IdentityApiClient>().AddDefaultPolicies();
        services.AddHttpClient<IMessageApiClient, MessageApiClient>().AddDefaultPolicies();
        return services;
    }
}
