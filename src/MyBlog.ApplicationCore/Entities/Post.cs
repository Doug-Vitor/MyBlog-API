public class Post : BaseEntity
{
    public string Content { get; set; }
    public DateTime? CreatedAt { get; set; }
    public int AuthorId { get; set; }
}