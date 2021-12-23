using System.ComponentModel;

public class PostDTO : BaseEntity
{
    public string Content { get; set; }
    public DateTime CreatedAt => DateTime.UtcNow;
    public int AuthorId { get; set; }
    public string AuthorUsername { get; set; }
}