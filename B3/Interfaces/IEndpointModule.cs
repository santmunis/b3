namespace B3.Interfaces;
public interface IEndpointModule
{
    IServiceCollection RegisterEndpoints(IServiceCollection services);
    IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder endpoints);
}

public static class ModuleExtensions
{
    private static readonly List<IEndpointModule> registeredModules = new();

    public static IServiceCollection RegisterEndpointModules(this IServiceCollection services)
    {
        var modules = DiscoverEndpointModules();
        foreach (var module in modules)
        {
            module.RegisterEndpoints(services);
            registeredModules.Add(module);
        }

        return services;
    }

    public static WebApplication MapEndpoints(this WebApplication app)
    {
        foreach (var module in registeredModules) module.MapEndpoints(app);
        return app;
    }

    //search for all modules that implements IModule interface
    private static IEnumerable<IEndpointModule> DiscoverEndpointModules()
    {
        return typeof(IEndpointModule).Assembly
            .GetTypes()
            .Where(p => p.IsClass && p.IsAssignableTo(typeof(IEndpointModule)))
            .Select(Activator.CreateInstance)
            .Cast<IEndpointModule>();
    }
}