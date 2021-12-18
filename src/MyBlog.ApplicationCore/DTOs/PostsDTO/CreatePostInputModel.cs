using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

public class CreatePostInputModel
{
    [DisplayName("Conteúdo")]
    [Required]
    public string Content { get; set; }
}