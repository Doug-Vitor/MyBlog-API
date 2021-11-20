using System.ComponentModel;

public class ErrorViewModel
{
    [DisplayName("Erros")]
    public List<string> ErrorsMessages { get; set; } = new();

    public ErrorViewModel()
    {
    }

    public ErrorViewModel(string errorMessage) => ErrorsMessages.Add(errorMessage);

    public ErrorViewModel(List<string> errorsMessages) => ErrorsMessages = errorsMessages;

    public void AddErrorMessage(string errorMessage) => ErrorsMessages.Add(errorMessage);
}
