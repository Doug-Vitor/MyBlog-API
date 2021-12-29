public class PostDTO : BaseEntity
{
    public string Content { get; set; }
    public DateTime CreatedAt => DateTime.UtcNow;
    public PostInteractorsDTO AuthorInfos { get; set; }
}