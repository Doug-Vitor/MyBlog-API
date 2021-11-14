using Microsoft.AspNetCore.Mvc;

public static class ApplicationBuilderExtensions
{
    public static async Task<WebApplication> CreateRolesAsync(this WebApplication webApplication)
    {
        SeedingServices seedingServices = webApplication.Services.GetRequiredService<SeedingServices>();
        await seedingServices.CreateRolesAsync();
        return webApplication;
    }
}
