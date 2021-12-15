using Microsoft.EntityFrameworkCore;
using System.Reflection;

public class AuthenticationContext : DbContext
{
    public DbSet<User> Users { get; set; }

    public AuthenticationContext(DbContextOptions<AuthenticationContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
