using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class PostsConfigurations : IEntityTypeConfiguration<Post>
{
    public void Configure(EntityTypeBuilder<Post> builder)
    {
        builder.HasKey(key => key.Id);
        builder.Property(prop => prop.Content).HasColumnType("ntext");
    }
}
