namespace AutoLot.Services.ApiWrapper.Configuration;

public static class ServiceConfiguration
{
    public static IServiceCollection ConfigureApiServiceWrapper(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<ApiServiceSettings>(configuration.GetSection(nameof(ApiServiceSettings)));
        services.AddHttpClient<ICarApiServiceWrapper, CarApiServiceWrapper>();
        services.AddHttpClient<IMakeApiServiceWrapper, MakeApiServiceWrapper>();
        return services;
    }
}
