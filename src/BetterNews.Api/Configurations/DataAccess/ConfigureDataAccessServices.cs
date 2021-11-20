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
        .GetConnectionString("Accounts")));

    private static IServiceCollection AddCoreDataServices(IServiceCollection services) => 
        services.AddScoped<IRoleRepository, RoleRepository>().AddScoped<IUserRepository, UserRepository>()
        .AddScoped<IUserServices, UserServices>().AddScoped<IUsersRolesRepository, UsersRolesRepository>()
        .AddScoped<CrossCuttingRepository>().AddScoped<ITokenServices, TokenServices>().AddScoped<SeedingServices>();
}
