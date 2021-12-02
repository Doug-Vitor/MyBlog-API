public class FieldInUseException : ApplicationException
{
    public FieldInUseException(string fieldName) : base($"O {fieldName} fornecido já existe.")
    {
    }
}
