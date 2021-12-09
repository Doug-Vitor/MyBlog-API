using System.ComponentModel;

public class ErrorDTO
{
    [DisplayName("Erros")]
    public List<string> ErrorsMessages { get; set; } = new();

    public ErrorDTO()
    {
    }

    public ErrorDTO(string errorMessage) => ErrorsMessages.Add(errorMessage);

    public ErrorDTO(List<string> errorsMessages) => ErrorsMessages = errorsMessages;

    public void AddErrorMessage(string errorMessage) => ErrorsMessages.Add(errorMessage);
}
