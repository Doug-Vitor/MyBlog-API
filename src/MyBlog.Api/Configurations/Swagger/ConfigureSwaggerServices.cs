using System.Reflection;

public static class ConfigureSwaggerServices
{
    internal static IServiceCollection ConfigureSwagger(this IServiceCollection services) => 
        services.AddSwaggerGen(options => options.IncludeXmlComments(Path.
            Combine(AppContext.BaseDirectory, $"{Assembly.GetExecutingAssembly().GetName().Name}.xml")));
}
