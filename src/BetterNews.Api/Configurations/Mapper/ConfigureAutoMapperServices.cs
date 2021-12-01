public static class ConfigureAutoMapperServices
{
    public static IServiceCollection ConfigureAutoMapper(this IServiceCollection services) 
        => services.AddAutoMapper(typeof(Program).Assembly);
}
