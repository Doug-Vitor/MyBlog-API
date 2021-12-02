using Microsoft.AspNetCore.Mvc;

public static class ApplicationBuilderExtensions
{
    public static async Task CreateRolesAsync(this IHost host)
    {
        using IServiceScope scope = host.Services.GetService<IServiceScopeFactory>().CreateScope();
        await scope.ServiceProvider.GetRequiredService<SeedingServices>().CreateRolesAsync();
    }
}
