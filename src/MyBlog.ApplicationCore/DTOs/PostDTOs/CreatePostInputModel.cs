using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

public class CreatePostInputModel
{
    [DisplayName("Conteúdo")]
    [Required(ErrorMessage = "Campo {0} é obrigatório")]
    public string Content { get; set; }

    [JsonIgnore]
    public DateTime? CreatedAt { get; set; } = DateTime.UtcNow;

}