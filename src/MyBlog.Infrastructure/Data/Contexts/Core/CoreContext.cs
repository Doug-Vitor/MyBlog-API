using Microsoft.EntityFrameworkCore;
using System.Reflection;

public  class CoreContext : DbContext
{
    public CoreContext(DbContextOptions<CoreContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
