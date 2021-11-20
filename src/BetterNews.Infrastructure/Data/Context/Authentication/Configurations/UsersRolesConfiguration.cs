using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class UsersRolesConfiguration : IEntityTypeConfiguration<UsersRoles>
{
    public void Configure(EntityTypeBuilder<UsersRoles> builder)
    {
        builder.HasKey(key => key.Id);
        builder.Property(prop => prop.UserId).IsRequired();
        builder.Property(prop => prop.RoleId).IsRequired();
    }
}
