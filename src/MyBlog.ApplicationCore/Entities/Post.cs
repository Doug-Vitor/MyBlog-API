public class Post : BaseEntity
{
    public string Content { get; set; }
    public int UserId { get; set; }

    public Post()
    {
    }
}