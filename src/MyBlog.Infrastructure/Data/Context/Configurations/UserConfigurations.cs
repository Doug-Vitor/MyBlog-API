using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class UserConfigurations : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(key => key.Id);

        builder.Property(prop => prop.Username).HasMaxLength(75).IsRequired();
        builder.HasIndex(idx => idx.Username).IsUnique();

        builder.Property(prop => prop.PasswordHash).HasMaxLength(64).IsRequired();

        builder.Property(prop => prop.Email).IsRequired();
        builder.HasIndex(idx => idx.Email).IsUnique();
    }
}