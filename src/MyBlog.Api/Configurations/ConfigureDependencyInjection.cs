public static class ConfigureDependencyInjection
{
    internal static IServiceCollection ConfigureDependencies(this IServiceCollection services,
        ConfigurationManager configurations) => services.ConfigureSwagger().ConfigureAutoMapper()
            .ConfigureAuthentication(configurations).ConfigureDataAccess(configurations);
}