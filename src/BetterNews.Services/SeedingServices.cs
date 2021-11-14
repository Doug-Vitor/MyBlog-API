using Microsoft.EntityFrameworkCore;

public class SeedingServices
{
    private readonly AuthenticationContext _authenticationContext;

    public SeedingServices(AuthenticationContext authenticationContext) => _authenticationContext = authenticationContext;

    public async Task CreateRolesAsync()
    {
        if (await _authenticationContext.Roles.AnyAsync()) return;
        await _authenticationContext.Roles.AddRangeAsync(new Role[] { new Role("User"), new Role("Journalist") });
    }
}
