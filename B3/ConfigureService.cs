namespace B3;

public static class ConfigureService
{
    public static IServiceCollection AddWebUiServices(this IServiceCollection services)
    {
        services.AddHttpContextAccessor();

        services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        
        
        services.AddEndpointsApiExplorer();
        
        return services;
    } 
}