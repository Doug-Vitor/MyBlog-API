public class Post : BaseEntity
{
    public string Content { get; set; }
    public int AuthorId { get; set; }

    public Post()
    {
    }
}