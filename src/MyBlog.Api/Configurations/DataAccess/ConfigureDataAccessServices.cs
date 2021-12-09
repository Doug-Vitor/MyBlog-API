using Microsoft.EntityFrameworkCore;

public static class ConfigureDataAccessServices
{
    public static IServiceCollection ConfigureDataAccess(this IServiceCollection services, ConfigurationManager configurations)
    {
        AddDbContext(services, configurations);
        AddCoreDataServices(services);
        return services;
    }

    private static IServiceCollection AddDbContext(IServiceCollection services, ConfigurationManager configurations) =>
        services.AddDbContext<AuthenticationContext>(options => options.UseSqlServer(configurations
        .GetConnectionString("Accounts"))).AddDbContext<CoreContext>(options => options.UseSqlServer(configurations
        .GetConnectionString("BlogDb")));

    private static IServiceCollection AddCoreDataServices(IServiceCollection services) => services.AddScoped<IUserRepository, UserRepository>()
        .AddScoped<IUserServices, UserServices>().AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>))
        .AddScoped<IPostServices, PostServices>().AddScoped<ITokenServices, TokenServices>();
}
