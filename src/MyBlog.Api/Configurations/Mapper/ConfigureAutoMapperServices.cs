public static class ConfigureAutoMapperServices
{
    internal static IServiceCollection ConfigureAutoMapper(this IServiceCollection services) 
        => services.AddAutoMapper(typeof(Program).Assembly);
}
