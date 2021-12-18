using System.ComponentModel;

public class PostDTO : BaseEntity
{
    [DisplayName("Conteúdo")]
    public string Content { get; set; }
    public int AuthorId { get; set; }

    [DisplayName("Autor")]
    public string AuthorUsername { get; set; }
}