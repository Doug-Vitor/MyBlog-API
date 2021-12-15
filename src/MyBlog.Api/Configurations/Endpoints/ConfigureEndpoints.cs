public static class ConfigureEndpoints
{
    public static IServiceCollection AddEndpoints(this IServiceCollection services, params Type[] scanMarkers)
    {
        List<IEndpoint> endpoints = new();
        foreach (Type marker in scanMarkers)
            endpoints.AddRange(marker.Assembly.ExportedTypes.Where(x => typeof(IEndpoint).IsAssignableFrom(x) && !(x.IsInterface && x.IsAbstract))
                .Select(Activator.CreateInstance).Cast<IEndpoint>());
        return services.AddSingleton(endpoints as IReadOnlyCollection<IEndpoint>);
    }

    public static void UseEndpoints(this WebApplication application)
    {
        foreach (IEndpoint endpoint in application.Services.GetRequiredService<IReadOnlyCollection<IEndpoint>>())
            endpoint.DefineEndpoints(application);
    }
}
